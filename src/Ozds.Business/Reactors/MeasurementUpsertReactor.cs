using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Iot.Entities.Abstractions;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors;

// TODO: transaction in sql and add measurement_location_id to the upsert

public class MeasurementUpsertReactor(
  IServiceScopeFactory serviceScopeFactory,
  IPushSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<PushEventArgs> channel =
    Channel.CreateUnbounded<PushEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribePush(OnPush);
    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribePush(OnPush);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnPush(object? sender, PushEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    PushEventArgs eventArgs
  )
  {
    await using var context = await serviceProvider
      .GetRequiredService<IDbContextFactory<DataDbContext>>()
      .CreateDbContextAsync();
    var pushRequestConverter = serviceProvider
      .GetRequiredService<AgnosticPushRequestMeasurementConverter>();
    var modelEntityConverter = serviceProvider
      .GetRequiredService<AgnosticModelEntityConverter>();
    var activator = serviceProvider
      .GetRequiredService<AgnosticModelActivator>();
    var aggregateUpserter =
      serviceProvider.GetRequiredService<AgnosticAggregateUpserter>();
    var aggregateConverter = serviceProvider
      .GetRequiredService<AgnosticMeasurementAggregateConverter>();
    var publisher =
      serviceProvider.GetRequiredService<IMeasurementUpsertPublisher>();
    var notificationQueries = serviceProvider
      .GetRequiredService<NotificationQueries>();
    var messengerJobManager = serviceProvider
      .GetRequiredService<IMessengerJobManager>();

    await RescheduleInactivityMonitorJob(
      messengerJobManager,
      context,
      eventArgs
    );

    var pushRequestsWithLocations =
      await GetMeterPushRequestsWithMeasurementLocations(
        context,
        eventArgs.Request.Measurements
      );

    IReadOnlyList<IMeasurement> upsertMeasurements =
      pushRequestsWithLocations
        .Select(x => pushRequestConverter
          .ToMeasurement(x.MeterPushRequest, x.MeasurementLocation.Id))
        .ToList();

    IReadOnlyList<IAggregate> upsertAggregates =
      MakeAggregates(aggregateUpserter, aggregateConverter, upsertMeasurements)
        .ToList();

    if (await Validate(
        context,
        upsertMeasurements,
        upsertAggregates) is { } validationResults)
    {
      var eventId = await AddPushEvent(
        context, activator, eventArgs, validationResults);
      await AddInvalidPushNotification(
        context,
        notificationQueries,
        activator,
        eventArgs,
        validationResults,
        eventId
      );
      return;
    }

    await AddPushEvent(context, activator, eventArgs);

    var tasks = MakeUpsertMeasurementTasks(
        context, modelEntityConverter, upsertMeasurements)
      .Concat(
        MakeUpsertAggregateTasks(
          context, modelEntityConverter, aggregateUpserter, upsertAggregates))
      .ToList();

    await ExecuteTransactionCommands(context, tasks);

    publisher.PublishUpsert(
      new MeasurementUpsertEventArgs
      {
        Measurements = upsertMeasurements,
        Aggregates = upsertAggregates
      });
  }

  private static async Task RescheduleInactivityMonitorJob(
    IMessengerJobManager manager,
    DataDbContext context,
    PushEventArgs eventArgs
  )
  {
    var messenger = (await context.Messengers
        .Where(context.PrimaryKeyEquals<MessengerEntity>(eventArgs.MessengerId))
        .FirstOrDefaultAsync())
      ?.ToModel();
    if (messenger is null)
    {
      return;
    }

    await manager.RescheduleInactivityMonitorJob(
      messenger.Id,
      messenger.MaxInactivityPeriod.ToTimeSpan()
    );
  }

  private static async Task<List<(
    IMeterPushRequestEntity MeterPushRequest,
    IMeasurementLocationEntity MeasurementLocation
  )>> GetMeterPushRequestsWithMeasurementLocations(
    DataDbContext context,
    IReadOnlyCollection<IMeterPushRequestEntity> pushRequestMeasurements)
  {
    var measurementLocations = await context.MeasurementLocations
      .Where(context.ForeignKeyIn<MeasurementLocationEntity>(
        nameof(MeasurementLocationEntity.Meter),
        pushRequestMeasurements
          .Select(x => x.MeterId)
          .Distinct()))
      .ToListAsync();

    var pushRequestMeasurementsWithLocation =
      new List<(
        IMeterPushRequestEntity Measurement,
        IMeasurementLocationEntity MeasurementLocation)>();
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

  private static IEnumerable<IAggregate> MakeAggregates(
    AgnosticAggregateUpserter aggregateUpserter,
    AgnosticMeasurementAggregateConverter aggregateConverter,
    IEnumerable<IMeasurement> measurements)
  {
    return measurements
      .SelectMany(
        model => Enum.GetValues<IntervalModel>()
          .Select(interval => aggregateConverter.ToAggregate(model, interval)))
      .OfType<IAggregate>()
      .GroupBy(
        aggregate => new
        {
          Type = aggregate.GetType(),
          aggregate.MeasurementLocationId,
          aggregate.MeterId,
          aggregate.Timestamp,
          aggregate.Interval
        })
      .Select(group => group.Aggregate(aggregateUpserter.UpsertModelAgnostic));
  }

  private static async Task<List<ValidationResult>?> Validate(
    DataDbContext context,
    IReadOnlyList<IMeasurement> validationMeasurements,
    IReadOnlyList<IAggregate> validationAggregates
  )
  {
    var meterIds = validationMeasurements.Select(x => x.MeterId)
      .Concat(validationAggregates.Select(x => x.MeterId))
      .Distinct()
      .ToList();

    var validators = await context.MeasurementValidators
      .Join(
        context.Meters.Where(context.PrimaryKeyIn<MeterEntity>(meterIds)),
        context.PrimaryKeyOf<MeasurementValidatorEntity>(),
        context.ForeignKeyOf<MeterEntity>(
          nameof(MeterEntity.MeasurementValidator)),
        (validator, _) => validator
      )
      .ToListAsync();

    var validationResults = new List<ValidationResult>();
    foreach (var validationMeasurement in validationMeasurements)
    {
      var validator = validators
        .FirstOrDefault(x => x.Id == validationMeasurement.MeterId);
      if (validator is null)
      {
        continue;
      }

      var validationContext = new ValidationContext(validationMeasurement)
      {
        Items = { ["MeasurementValidator"] = validator }
      };

      validationResults.AddRange(
        validationMeasurement
          .Validate(validationContext));
    }

    if (validationResults.Count is not 0)
    {
      return validationResults;
    }

    return null;
  }

  private static async Task<string?> AddPushEvent(
    DataDbContext context,
    AgnosticModelActivator activator,
    PushEventArgs eventArgs,
    List<ValidationResult>? validationResults = null)
  {
    var messenger = (await context.Messengers
        .Where(context.PrimaryKeyEquals<MessengerEntity>(eventArgs.MessengerId))
        .FirstOrDefaultAsync())
      ?.ToModel();
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
    var eventEntity = @event.ToEntity();
    context.Add(eventEntity);
    await context.SaveChangesAsync();

    return @event.Id;
  }

  private static async Task AddInvalidPushNotification(
    DataDbContext context,
    NotificationQueries queries,
    AgnosticModelActivator activator,
    PushEventArgs eventArgs,
    List<ValidationResult> validationResults,
    string? eventId = null)
  {
    var messenger = (await context.Messengers
        .Where(context.PrimaryKeyEquals<MessengerEntity>(eventArgs.MessengerId))
        .FirstOrDefaultAsync())
      ?.ToModel();
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
    var notificationEntity = notification.ToEntity();
    context.Add(notificationEntity);
    await context.SaveChangesAsync();
    notification.Id = notificationEntity.Id;

    var recipients = await queries.Recipients(notification);
    context.AddRange(recipients);
    await context.SaveChangesAsync();
  }

  private static async Task ExecuteTransactionCommands(
    DataDbContext context,
    IEnumerable<Func<Task>> tasks)
  {
    while (true)
    {
      try
      {
        var isolationLevel = IsolationLevel.RepeatableRead;
        await context.Database.BeginTransactionAsync(isolationLevel);
        foreach (var task in tasks)
        {
          await task();
        }

        await context.Database.CommitTransactionAsync();
        break;
      }
      catch (PostgresException ex)
      {
        // NOTE: serialization failure
        if (ex.Message.StartsWith("40001"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        // NOTE: deadlock detected
        if (ex.Message.StartsWith("40P01"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        // NOTE: inserts don't return rows with concurrency issues
        if (ex.Message.StartsWith("P0002"))
        {
          await context.Database.RollbackTransactionAsync();
          continue;
        }

        throw;
      }
    }
  }

  private static IEnumerable<Func<Task>> MakeUpsertMeasurementTasks(
    DataDbContext context,
    AgnosticModelEntityConverter modelEntityConverter,
    IEnumerable<IMeasurement> measurements)
  {
    foreach (var group in measurements
      .Select(modelEntityConverter.ToEntity)
      .GroupBy(measurement => measurement.GetType()))
    {
      var enumerableCastMethod = typeof(Enumerable)
          .GetMethod(
            nameof(Enumerable.Cast),
            BindingFlags.Public | BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertMeasurementsMethod = typeof(MeasurementUpsertReactor)
          .GetMethod(
            nameof(UpsertMeasurements),
#pragma warning disable S3011
            BindingFlags.NonPublic |
#pragma warning restore S3011
            BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(UpsertMeasurements)}.");

      yield return () =>
        (upsertMeasurementsMethod.Invoke(
          null,
          [
            context,
            enumerableCastMethod.Invoke(null, [group])
            ?? throw new InvalidOperationException(
              $"Cannot cast group to {group.Key.Name}.")
          ]) as Task)!;
    }
  }

  private static IEnumerable<Func<Task>> MakeUpsertAggregateTasks(
    DataDbContext context,
    AgnosticModelEntityConverter modelEntityConverter,
    AgnosticAggregateUpserter aggregateUpserter,
    IEnumerable<IAggregate> aggregates)
  {
    foreach (var group in aggregates
      .Select(modelEntityConverter.ToEntity)
      .GroupBy(entity => entity.GetType()))
    {
      var enumerableCastMethod = typeof(Enumerable)
          .GetMethod(
            nameof(Enumerable.Cast),
            BindingFlags.Public | BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertAggregatesMethod = typeof(MeasurementUpsertReactor)
          .GetMethod(
            nameof(UpsertAggregates),
#pragma warning disable S3011
            BindingFlags.NonPublic |
#pragma warning restore S3011
            BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(UpsertAggregates)}.");

      yield return () =>
        (upsertAggregatesMethod.Invoke(
          null,
          [
            context,
            enumerableCastMethod.Invoke(null, [group])
            ?? throw new InvalidOperationException(
              $"Cannot cast group to {group.Key.Name}."),
            aggregateUpserter
          ]) as Task)!;
    }
  }

  private static async Task UpsertMeasurements<T>(
    DataDbContext context,
    IEnumerable<T> measurements
  )
    where T : class, IMeasurementEntity
  {
    await context
      .UpsertRange(measurements.ToArray())
      .On(
        measurement => new
        {
          measurement.MeasurementLocationId,
          measurement.MeterId,
          measurement.Timestamp
        })
      .NoUpdate()
      .RunAsync();
  }

  private static async Task UpsertAggregates<T>(
    DataDbContext context,
    IEnumerable<T> aggregates,
    AgnosticAggregateUpserter upserter)
    where T : class, IAggregateEntity
  {
    await context
      .UpsertRange(aggregates.ToArray())
      .On(
        aggregate => new
        {
          aggregate.MeasurementLocationId,
          aggregate.MeterId,
          aggregate.Timestamp,
          aggregate.Interval
        })
      .WhenMatched(upserter.UpsertEntity<T>())
      .RunAsync();
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
