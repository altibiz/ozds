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
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Business.Validation;
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
  ModelValidator validator,
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
    await RescheduleInactivityMonitorJob(eventArgs, cancellationToken);

    var validationResults = await Validate(
      eventArgs.Measurements,
      cancellationToken);
    if (validationResults?.Count > 0)
    {
      var eventId = await AddPushEvent(
        eventArgs,
        validationResults,
        cancellationToken);
      await AddInvalidPushNotification(
        eventArgs,
        validationResults,
        eventId,
        cancellationToken);
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
    var validationResults = new List<ValidationResult>();
    foreach (var measurement in measurements)
    {
      var validationResult = await validator.ValidateAsync(
        measurement,
        cancellationToken
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
    IotPushEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(
        eventArgs.MessengerId,
        cancellationToken);
    if (messenger is null)
    {
      return;
    }

    await messengerJobManager.RescheduleInactivityMonitorJob(
      messenger.Id,
      messenger.MaxInactivityPeriod.ToTimeSpan(),
      cancellationToken
    );
  }

  private async Task<string?> AddPushEvent(
    IotPushEventArgs eventArgs,
    List<ValidationResult>? validationResults,
    CancellationToken cancellationToken)
  {
    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(
        eventArgs.MessengerId,
        cancellationToken);
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
      .Create(@event, cancellationToken);

    return @event.Id;
  }

  private async Task AddInvalidPushNotification(
    IotPushEventArgs eventArgs,
    List<ValidationResult> validationResults,
    string? eventId,
    CancellationToken cancellationToken)
  {
    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(
        eventArgs.MessengerId,
        cancellationToken);
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
      .Create(notification, cancellationToken);

    var recipients = await notificationQueries.Recipients(notification);
    await notificationMutations
      .AddRecipients(recipients, cancellationToken);
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
