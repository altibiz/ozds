using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

// FIXME: duplicates make averages incorrect

namespace Ozds.Business.Aggregation;

public class BatchAggregatedMeasurementUpserter(
  AgnosticMeasurementAggregateConverter aggregateConverter,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticAggregateUpserter aggregateUpserter,
  DataDbContext context
)
{
  private static readonly SemaphoreSlim Semaphore = new(1, 1);

  public async Task BatchAggregatedUpsert(
    IEnumerable<IMeasurement> measurements
  )
  {
    var tasks = new List<Func<Task>>();

    var measurementGroups = measurements
      .Select(modelEntityConverter.ToEntity)
      .GroupBy(measurement => measurement.GetType());

    foreach (var group in measurementGroups)
    {
      var enumerableCastMethod = typeof(Enumerable)
          .GetMethod(
            nameof(Enumerable.Cast),
            BindingFlags.Public | BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertMeasurementsMethod = typeof(BatchAggregatedMeasurementUpserter)
          .GetMethod(
            nameof(UpsertMeasurements),
#pragma warning disable S3011
            BindingFlags.NonPublic |
#pragma warning restore S3011
            BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(UpsertMeasurements)}.");

      tasks.Add(
        () =>
          (upsertMeasurementsMethod.Invoke(
            null,
            [
              context,
              enumerableCastMethod.Invoke(null, [group])
              ?? throw new InvalidOperationException(
                $"Cannot cast group to {group.Key.Name}.")
            ]) as Task)!
      );
    }

    var aggregateGroups = measurements
      .SelectMany(
        model => typeof(IntervalModel)
          .GetEnumValues()
          .Cast<IntervalModel>()
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
      .Select(group => group.Aggregate(aggregateUpserter.UpsertModelAgnostic))
      .Select(modelEntityConverter.ToEntity)
      .OfType<IAggregateEntity>()
      .GroupBy(entity => entity.GetType());

    foreach (var group in aggregateGroups)
    {
      var enumerableCastMethod = typeof(Enumerable)
          .GetMethod(
            nameof(Enumerable.Cast),
            BindingFlags.Public | BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertAggregatesMethod = typeof(BatchAggregatedMeasurementUpserter)
          .GetMethod(
            nameof(UpsertAggregates),
#pragma warning disable S3011
            BindingFlags.NonPublic |
#pragma warning restore S3011
            BindingFlags.Static)
          ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(UpsertAggregates)}.");

      tasks.Add(
        () =>
          (upsertAggregatesMethod.Invoke(
            null,
            [
              context,
              enumerableCastMethod.Invoke(null, [group])
              ?? throw new InvalidOperationException(
                $"Cannot cast group to {group.Key.Name}."),
              aggregateUpserter
            ]) as Task)!
      );
    }

    await Semaphore.WaitAsync();
    while (true)
    {
      try
      {
        await context.Database.BeginTransactionAsync(
          IsolationLevel.RepeatableRead);
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

    Semaphore.Release();
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
