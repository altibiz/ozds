using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Buffers;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Agnostic;
using Ozds.Business.Reactors.Base;
using Ozds.Business.Validation.Agnostic;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors;

public class IotPushReactor(
  IServiceProvider serviceProvider
) : Reactor<IotPushEventArgs, IIotPushSubscriber, IotPushHandler>(
  serviceProvider
)
{
}

public class IotPushHandler(
  AgnosticValidator validator,
  AgnosticModelActivator activator,
  MeasurementBuffer buffer,
  NotificationMutations notificationMutations,
  AuditableQueries auditableQueries,
  NotificationQueries notificationQueries,
  IMessengerJobManager messengerJobManager,
  ReadonlyMutations readonlyMutations
) : Handler<IotPushEventArgs>
{
  public override async Task Handle(
    IotPushEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    await RescheduleInactivityMonitorJob(eventArgs);

    var validationResults = await Validate(eventArgs.Measurements);
    if (validationResults?.Count > 0)
    {
      var eventId = await AddPushEvent(eventArgs, validationResults);
      await AddInvalidPushNotification(eventArgs, validationResults, eventId);
      return;
    }

    buffer.Add(eventArgs.Measurements, eventArgs.BufferBehavior);

    await AddPushEvent(eventArgs);
  }

  private async Task<List<ValidationResult>?> Validate(
    IEnumerable<IMeasurement> measurements
  )
  {
    var validationResults = new List<ValidationResult>();
    foreach (var measurement in measurements)
    {
      var validationResult = await validator.ValidateAsync(
        measurement,
        CancellationToken.None
      );

      validationResults.AddRange(
        validationResult);
    }

    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    return null;
  }

  private async Task RescheduleInactivityMonitorJob(
    IotPushEventArgs eventArgs
  )
  {
    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(
        eventArgs.MessengerId,
        CancellationToken.None);
    if (messenger is null)
    {
      return;
    }

    await messengerJobManager.RescheduleInactivityMonitorJob(
      messenger.Id,
      messenger.MaxInactivityPeriod.ToTimeSpan()
    );
  }

  private async Task<string?> AddPushEvent(
    IotPushEventArgs eventArgs,
    List<ValidationResult>? validationResults = null)
  {
    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(
        eventArgs.MessengerId,
        CancellationToken.None);
    if (messenger is null)
    {
      return null;
    }

    var @event = activator.Activate<MessengerEventModel>();
    @event.MessengerId = messenger.Id;
    @event.Timestamp = DateTimeOffset.UtcNow;
    @event.Categories =
    [
      CategoryModel.All,
      CategoryModel.Messenger,
      CategoryModel.MessengerPush
    ];
    @event.Content = CreateEventContent(eventArgs, messenger);
    @event.Level = validationResults is null
      ? LevelModel.Information
      : LevelModel.Error;
    @event.Title = validationResults is null
      ? "Messenger \"{messenger.Title}\" pushed"
      : "Messenger \"{messenger.Title}\" pushed with validation errors";
    @event.Id = await readonlyMutations
      .Create(@event, CancellationToken.None);

    return @event.Id;
  }

  private async Task AddInvalidPushNotification(
    IotPushEventArgs eventArgs,
    List<ValidationResult> validationResults,
    string? eventId = null)
  {
    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(
        eventArgs.MessengerId,
        CancellationToken.None);
    if (messenger is null)
    {
      return;
    }

    var notification = activator.Activate<MessengerNotificationModel>();
    notification.MessengerId = messenger.Id;
    notification.Timestamp = DateTimeOffset.UtcNow;
    notification.Topics =
    [
      TopicModel.All,
      TopicModel.InvalidPush
    ];
    notification.Summary = $"Messenger \"{messenger.Title}\" push failed";
    notification.Content = string.Join(
      "\n", validationResults
        .Select(x => $"{x.MemberNames.First()}: {x.ErrorMessage}"));
    notification.EventId = eventId;
    notification.Id = await notificationMutations
      .Create(notification, CancellationToken.None);

    var recipients = await notificationQueries.Recipients(notification);
    await notificationMutations
      .AddRecipients(recipients, CancellationToken.None);
  }

  private static JsonDocument CreateEventContent(
    IotPushEventArgs eventArgs,
    IMessenger messenger)
  {
    var content = new EventContent(
      messenger.Id,
      eventArgs.Measurements.Count,
      eventArgs.Measurements
        .GroupBy(x => x.MeterId)
        .Select(group => new EventContentMeter(group.Key, group.Count()))
        .ToArray()
    );

    return JsonSerializer.SerializeToDocument(content);
  }

  private sealed record EventContent(
    string MessengerId,
    int Count,
    EventContentMeter[] Meters
  );

  private sealed record EventContentMeter(
    string Id,
    int Count
  );
}
