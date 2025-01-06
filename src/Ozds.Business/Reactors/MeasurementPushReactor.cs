using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json;
using System.Threading.Channels;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Buffers;
using Ozds.Business.Caching;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Agnostic;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Validation.Agnostic;
using Ozds.Iot.Entities.Abstractions;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors;

// TODO: transaction in sql and add measurement_location_id to the upsert

public class MeasurementPushReactor(
  IServiceScopeFactory serviceScopeFactory,
  AgnosticValidator validator,
  IPushSubscriber subscriber,
  ILogger<MeasurementPushReactor> logger
) : BackgroundService, IReactor
{
  private readonly Channel<PushEventArgs> channel =
    Channel.CreateUnbounded<PushEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribePush(OnPush);
    await base.StartAsync(cancellationToken);
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribePush(OnPush);
    await base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      try
      {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        await Handle(scope.ServiceProvider, eventArgs);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Measurement upsert failed for {Count} measurements",
          eventArgs.Request.Measurements.Count);
      }
    }
  }

  private void OnPush(object? sender, PushEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private async Task Handle(
    IServiceProvider serviceProvider,
    PushEventArgs eventArgs
  )
  {
    var pushRequestConverter = serviceProvider
      .GetRequiredService<AgnosticPushRequestMeasurementConverter>();
    var activator = serviceProvider
      .GetRequiredService<AgnosticModelActivator>();
    var buffer = serviceProvider
      .GetRequiredService<MeasurementBuffer>();
    var notificationMutations = serviceProvider
      .GetRequiredService<NotificationMutations>();
    var auditableQueries = serviceProvider
      .GetRequiredService<AuditableQueries>();
    var notificationQueries = serviceProvider
      .GetRequiredService<NotificationQueries>();
    var messengerJobManager = serviceProvider
      .GetRequiredService<IMessengerJobManager>();
    var readonlyMutations = serviceProvider
      .GetRequiredService<ReadonlyMutations>();
    var measurementLocationCache = serviceProvider
      .GetRequiredService<MeasurementLocationCache>();

    await RescheduleInactivityMonitorJob(
      messengerJobManager,
      auditableQueries,
      eventArgs
    );

    var pushRequestsWithLocations =
      await GetMeterPushRequestsWithMeasurementLocations(
        measurementLocationCache,
        eventArgs.Request.Measurements
      );

    var measurements =
      pushRequestsWithLocations
        .Select(x => pushRequestConverter
          .ToMeasurement(x.MeterPushRequest, x.MeasurementLocation.Id))
        .ToList();

    var validationResults = await Validate(measurements);
    if (validationResults?.Count > 0)
    {
      var eventId = await AddPushEvent(
        auditableQueries,
        readonlyMutations,
        activator,
        eventArgs,
        validationResults);
      await AddInvalidPushNotification(
        notificationMutations,
        auditableQueries,
        notificationQueries,
        activator,
        eventArgs,
        validationResults,
        eventId);
      return;
    }

    buffer.Add(
      measurements,
      eventArgs.BufferBehavior == PushEventBufferBehavior.Realtime
        ? MeasurementBufferBehavior.Realtime
        : MeasurementBufferBehavior.Buffer);

    await AddPushEvent(
      auditableQueries,
      readonlyMutations,
      activator,
      eventArgs);
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

  private static async Task RescheduleInactivityMonitorJob(
    IMessengerJobManager manager,
    AuditableQueries auditableQueries,
    PushEventArgs eventArgs
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

    await manager.RescheduleInactivityMonitorJob(
      messenger.Id,
      messenger.MaxInactivityPeriod.ToTimeSpan()
    );
  }

  // TODO: cache this
  private static async Task<List<(
    IMeterPushRequestEntity MeterPushRequest,
    IMeasurementLocation MeasurementLocation
  )>> GetMeterPushRequestsWithMeasurementLocations(
    MeasurementLocationCache cache,
    IReadOnlyCollection<IMeterPushRequestEntity> pushRequestMeasurements)
  {
    var measurementLocations = await cache.GetAsync(
      pushRequestMeasurements
        .Select(x => x.MeterId)
        .Distinct(),
      CancellationToken.None);

    var pushRequestMeasurementsWithLocation =
      new List<(
        IMeterPushRequestEntity Measurement,
        IMeasurementLocation MeasurementLocation)>();
    foreach (var measurement in pushRequestMeasurements)
    {
      var measurementLocation = measurementLocations
        .FirstOrDefault(x => x.MeterId == measurement.MeterId);
      if (measurementLocation is null)
      {
        continue;
      }

      pushRequestMeasurementsWithLocation.Add(
        (measurement, measurementLocation));
    }

    return pushRequestMeasurementsWithLocation;
  }

  private static async Task<string?> AddPushEvent(
    AuditableQueries auditableQueries,
    ReadonlyMutations readonlyMutations,
    AgnosticModelActivator activator,
    PushEventArgs eventArgs,
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

  private static async Task AddInvalidPushNotification(
    NotificationMutations mutations,
    AuditableQueries auditableQueries,
    NotificationQueries notificationQueries,
    AgnosticModelActivator activator,
    PushEventArgs eventArgs,
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
    notification.Id = await mutations
      .Create(notification, CancellationToken.None);

    var recipients = await notificationQueries.Recipients(notification);
    await mutations.AddRecipients(recipients, CancellationToken.None);
  }

  private static JsonDocument CreateEventContent(
    PushEventArgs eventArgs,
    IMessenger messenger)
  {
    var content = new EventContent(
      messenger.Id,
      eventArgs.Request.Measurements.Count,
      eventArgs.Request.Measurements
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
