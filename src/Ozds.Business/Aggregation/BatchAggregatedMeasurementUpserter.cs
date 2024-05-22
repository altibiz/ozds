using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Data;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Aggregation;

public class BatchAggregatedMeasurementUpserter(
  AgnosticMeasurementAggregateConverter aggregateConverter,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticAggregateUpserter aggregateUpserter,
  OzdsDbContext context
)
{
  public async Task BatchAggregatedUpsert(
    IEnumerable<IMeasurement> measurements
  )
  {
    var measurementGroups = measurements
      .Select(modelEntityConverter.ToEntity)
      .GroupBy(measurement => measurement.GetType());

    foreach (var group in measurementGroups)
    {
      var enumerableCastMethod = typeof(Enumerable)
                                   .GetMethod(nameof(Enumerable.Cast),
                                     BindingFlags.Public | BindingFlags.Static)
                                   ?.MakeGenericMethod(group.Key)
                                 ?? throw new InvalidOperationException(
                                   $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertMeasurementsMethod = typeof(BatchAggregatedMeasurementUpserter)
                                       .GetMethod(nameof(UpsertMeasurements),
                                         BindingFlags.NonPublic |
                                         BindingFlags.Static)
                                       ?.MakeGenericMethod(group.Key)
                                     ?? throw new InvalidOperationException(
                                       $"Cannot find method {nameof(UpsertMeasurements)}.");
      var result = upsertMeasurementsMethod.Invoke(null,
      [
        context,
        enumerableCastMethod.Invoke(null, [group])
        ?? throw new InvalidOperationException(
          $"Cannot cast group to {group.Key.Name}.")
      ]);

      if (result is Task task)
      {
        await task;
      }
    }

    var aggregateGroups = measurements
      .SelectMany(model => typeof(IntervalModel)
        .GetEnumValues()
        .Cast<IntervalModel>()
        .Select(interval => aggregateConverter.ToAggregate(model, interval)))
      .OfType<IAggregate>()
      .GroupBy(aggregate => new
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
                                   .GetMethod(nameof(Enumerable.Cast),
                                     BindingFlags.Public | BindingFlags.Static)
                                   ?.MakeGenericMethod(group.Key)
                                 ?? throw new InvalidOperationException(
                                   $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertAggregatesMethod = typeof(BatchAggregatedMeasurementUpserter)
                                     .GetMethod(nameof(UpsertAggregates),
                                       BindingFlags.NonPublic |
                                       BindingFlags.Static)
                                     ?.MakeGenericMethod(group.Key)
                                   ?? throw new InvalidOperationException(
                                     $"Cannot find method {nameof(UpsertAggregates)}.");
      var result = upsertAggregatesMethod.Invoke(null,
      [
        context,
        enumerableCastMethod.Invoke(null, [group])
        ?? throw new InvalidOperationException(
          $"Cannot cast group to {group.Key.Name}."),
        aggregateUpserter
      ]);

      if (result is Task task)
      {
        await task;
      }
    }
  }

  private static Task<int> UpsertMeasurements<T>(
    DbContext context,
    IEnumerable<T> measurements
  )
    where T : class, IMeasurementEntity
  {
    return context
      .UpsertRange(measurements.ToArray())
      .On(measurement => new
      {
        measurement.MeterId,
        measurement.Timestamp
      })
      .NoUpdate()
      .RunAsync();
  }

  private static Task<int> UpsertAggregates<T>(
    DbContext context,
    IEnumerable<T> aggregates,
    AgnosticAggregateUpserter upserter)
    where T : class, IAggregateEntity
  {
    return context
      .UpsertRange(aggregates.ToArray())
      .On(aggregate => new
      {
        aggregate.MeterId,
        aggregate.Timestamp,
        aggregate.Interval
      })
      .WhenMatched(upserter.UpsertEntity<T>())
      .RunAsync();
  }
}
