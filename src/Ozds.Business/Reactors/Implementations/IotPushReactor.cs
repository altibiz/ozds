using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Ozds.Business.Activation;
using Ozds.Business.Buffers;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Business.Validation;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

public class IotPushReactor(IServiceProvider serviceProvider)
  : Reactor<IotPushEventArgs, IIotPushSubscriber, IotPushHandler>(
    serviceProvider
  ) { }

public class IotPushHandler(
  ModelValidator validator,
  ModelActivator activator,
  MeasurementBuffer buffer,
  IMessengerJobManager messengerJobManager,
  IMeterJobManager meterJobManager,
  ModelMutations modelMutations,
  TrackableQueries trackableQueries,
  ClockQueries clock,
  TimeQueries time
) : Handler<IotPushEventArgs>
{
  public override async Task Handle(
    IotPushEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    await RescheduleInactivityMonitorJobs(eventArgs, cancellationToken);

    var validationResults = await Validate(
      eventArgs.Measurements,
      cancellationToken
    );
    if (validationResults?.Count > 0)
    {
      var eventId = await AddPushEvent(
        eventArgs,
        validationResults,
        cancellationToken
      );
      await AddInvalidPushNotification(
        eventArgs,
        validationResults,
        eventId,
        cancellationToken
      );
      return;
    }

    buffer.Add(eventArgs.Measurements, eventArgs.BufferBehavior);

    await AddPushEvent(eventArgs, null, cancellationToken);
  }

  private async Task<List<ValidationResult>?> Validate(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    var validationResults = await validator.Validate(
      measurements,
      cancellationToken
    );

    if (validationResults.Count > 0)
    {
      return validationResults;
    }

    return null;
  }

  private async Task RescheduleInactivityMonitorJobs(
    IotPushEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    var messenger = await trackableQueries.ReadById<IMessenger>(
      eventArgs.MessengerId,
      cancellationToken
    );
    if (messenger is not null)
    {
      await messengerJobManager.RescheduleInactivityMonitorJob(
        new MessengerInactivityMonitorDetails(
          messenger.Id,
          time.PeriodTimeSpan(messenger.MaxInactivityPeriod)
        ),
        cancellationToken
      );
    }

    var meterIds = eventArgs.Measurements.Select(x => x.MeterId).Distinct();
    var meters = await trackableQueries.ReadByIds<IMeter>(
      meterIds,
      cancellationToken
    );
    if (meters.Count > 0)
    {
      await meterJobManager.RescheduleInactivityMonitorJobs(
        meters.Select(x => new MeterInactivityMonitorDetails(
          x.Id,
          time.PeriodTimeSpan(x.MaxInactivityPeriod)
        )),
        cancellationToken
      );
    }
  }

  private async Task<string?> AddPushEvent(
    IotPushEventArgs eventArgs,
    List<ValidationResult>? validationResults,
    CancellationToken cancellationToken
  )
  {
    var now = clock.Timestamp();

    var messenger = await trackableQueries.ReadById<IMessenger>(
      eventArgs.MessengerId,
      cancellationToken
    );
    if (messenger is null)
    {
      return null;
    }

    var @event = activator.Activate<MessengerEventModel>();
    @event.MessengerId = messenger.Id;
    @event.Timestamp = now;
    @event.Categories =
    [
      CategoryModel.All,
      CategoryModel.Messenger,
      CategoryModel.MessengerPush,
    ];
    var error = validationResults is not null
      ? string.Join(
        "\n",
        validationResults.Select(x =>
          $"{x.MemberNames.First()}: {x.ErrorMessage}"
        )
      )
      : null;
    @event.Content = CreateEventContent(eventArgs, messenger, error);
    @event.Level = validationResults is null
      ? LevelModel.Information
      : LevelModel.Error;
    @event.Title = validationResults is null
      ? $"Messenger '{messenger.Title}' pushed"
      : $"Messenger '{messenger.Title}' pushed with validation errors";
    await modelMutations.Create(@event, cancellationToken);

    return @event.Id;
  }

  private async Task AddInvalidPushNotification(
    IotPushEventArgs eventArgs,
    List<ValidationResult> validationResults,
    string? eventId,
    CancellationToken cancellationToken
  )
  {
    var now = clock.Timestamp();

    var messenger = await trackableQueries.ReadById<IMessenger>(
      eventArgs.MessengerId,
      cancellationToken
    );
    if (messenger is null)
    {
      return;
    }

    var notification = activator.Activate<MessengerNotificationModel>();
    notification.MessengerId = messenger.Id;
    notification.Timestamp = now;
    notification.Topics = [TopicModel.All, TopicModel.InvalidPush];
    notification.Summary = $"Messenger \"{messenger.Title}\" push failed";
    // NOTE: \n is ok here because we're storing it in the database
    notification.Content = string.Join(
      "\n",
      validationResults.Select(x =>
        $"{x.MemberNames.First()}: {x.ErrorMessage}"
      )
    );
    notification.EventId = eventId;
    await modelMutations.Create(notification, cancellationToken);
  }

  private static JsonDocument CreateEventContent(
    IotPushEventArgs eventArgs,
    IMessenger messenger,
    string? error
  )
  {
    var content = new EventContent(
      messenger.Id,
      eventArgs.Measurements.Count,
      eventArgs
        .Measurements.GroupBy(x => x.MeterId)
        .Select(group => new EventContentMeter(group.Key, group.Count()))
        .ToArray(),
      error
    );

    return JsonSerializer.SerializeToDocument(content);
  }

  private sealed record EventContent(
    string MessengerId,
    int Count,
    EventContentMeter[] Meters,
    string? Error
  );

  private sealed record EventContentMeter(string Id, int Count);
}
