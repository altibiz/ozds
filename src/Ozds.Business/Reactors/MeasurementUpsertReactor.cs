using System.Data;
using System.Reflection;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class MeasurementUpsertWorker(
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
    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var pushRequestConverter = serviceProvider
      .GetRequiredService<AgnosticPushRequestMeasurementConverter>();
    var modelEntityConverter = serviceProvider
      .GetRequiredService<AgnosticModelEntityConverter>();
    var aggregateUpserter =
      serviceProvider.GetRequiredService<AgnosticAggregateUpserter>();
    var aggregateConverter = serviceProvider
      .GetRequiredService<AgnosticMeasurementAggregateConverter>();
    var publisher = serviceProvider.GetRequiredService<IMeasurementUpsertPublisher>();

    IReadOnlyList<IMeasurement> upsertMeasurements =
      eventArgs.Request.Measurements
        .Select(pushRequestConverter.ToMeasurement)
        .ToList();

    IReadOnlyList<IAggregate> upsertAggregates =
      MakeAggregates(aggregateUpserter, aggregateConverter, upsertMeasurements)
        .ToList();

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
          aggregate.MeterId,
          aggregate.Timestamp,
          aggregate.Interval
        })
      .Select(group => group.Aggregate(aggregateUpserter.UpsertModelAgnostic));
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
      var upsertMeasurementsMethod = typeof(MeasurementUpsertWorker)
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
      var upsertAggregatesMethod = typeof(MeasurementUpsertWorker)
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
          aggregate.MeterId,
          aggregate.Timestamp,
          aggregate.Interval
        })
      .WhenMatched(upserter.UpsertEntity<T>())
      .RunAsync();
  }
}
