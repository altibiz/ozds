using System.Data;
using System.Reflection;
using Enms.Business.Aggregation.Agnostic;
using Enms.Business.Conversion.Agnostic;
using Enms.Business.Models.Abstractions;
using Enms.Business.Models.Enums;
using Enms.Business.Pushing.Abstractions;
using Enms.Data;
using Enms.Data.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

// FIXME: duplicates make averages incorrect

namespace Enms.Business.Pushing;

public class MeasurementPusher(
  AgnosticMeasurementAggregateConverter aggregateConverter,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticAggregateUpserter aggregateUpserter,
  EnmsDataDbContext context,
  IMeasurementPublisher publisher
) : IMeasurementPusher
{
  private static readonly SemaphoreSlim Semaphore = new(1, 1);

  public async Task Push(IEnumerable<IMeasurement> measurements)
  {
    IReadOnlyList<IMeasurement> publishMeasurements = measurements.ToList();

    IReadOnlyList<IAggregate> publishAggregates =
      MakeAggregates(publishMeasurements).ToList();

    publisher.BeforePublish(publishMeasurements, publishAggregates);

    var tasks = MakeUpsertMeasurementTasks(publishMeasurements)
      .Concat(MakeUpsertAggregateTasks(publishAggregates))
      .ToList();

    await Semaphore.WaitAsync();

    await ExecuteTransactionCommands(tasks);

    Semaphore.Release();

    publisher.AfterPublish(publishMeasurements, publishAggregates);
  }

  private IEnumerable<IAggregate> MakeAggregates(
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
          aggregate.LineId,
          aggregate.Timestamp,
          aggregate.Interval
        })
      .Select(group => group.Aggregate(aggregateUpserter.UpsertModelAgnostic));
  }

  private async Task ExecuteTransactionCommands(IEnumerable<Func<Task>> tasks)
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

  private IEnumerable<Func<Task>> MakeUpsertMeasurementTasks(
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
      var upsertMeasurementsMethod = typeof(MeasurementPusher)
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

  private IEnumerable<Func<Task>> MakeUpsertAggregateTasks(
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
      var upsertAggregatesMethod = typeof(MeasurementPusher)
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
    EnmsDataDbContext context,
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
          measurement.LineId,
          measurement.Timestamp
        })
      .NoUpdate()
      .RunAsync();
  }

  private static async Task UpsertAggregates<T>(
    EnmsDataDbContext context,
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
          aggregate.LineId,
          aggregate.Timestamp,
          aggregate.Interval
        })
      .WhenMatched(upserter.UpsertEntity<T>())
      .RunAsync();
  }
}
