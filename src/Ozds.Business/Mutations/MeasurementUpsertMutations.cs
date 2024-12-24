using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Validation.Agnostic;
using Ozds.Data.Entities.Abstractions;
using DataMeasurementUpsertMutations = Ozds.Data.Mutations.MeasurementUpsertMutations;

namespace Ozds.Business.Mutations;

public record UpsertMeasurementsResult(
  List<IMeasurement> Measurements,
  List<ValidationResult> ValidationResults
);

public class MeasurementUpsertMutations(
  DataMeasurementUpsertMutations mutations,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticMeasurementAggregateConverter aggregateConverter,
  AgnosticAggregateUpserter aggregateUpserter,
  AgnosticValidator validator,
  IAggregateFlushPublisher publisher,
  ILogger<MeasurementUpsertMutations> logger
) : IMutations
{
  public async Task<UpsertMeasurementsResult> UpsertMeasurements(
    IEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    measurements = measurements.ToList();

    var stopwatch = Stopwatch.StartNew();
    var validationResults = await Validate(measurements);
    stopwatch.Stop();
    logger.LogDebug(
      "Validated {Count} measurements in {Elapsed}",
      measurements.Count(),
      stopwatch.Elapsed);
    if (validationResults is { })
    {
      return new UpsertMeasurementsResult(
        measurements.ToList(),
        validationResults
      );
    }

    var aggregates = measurements
      .SelectMany(x => Enum.GetValues<IntervalModel>()
        .Select(interval => aggregateConverter.ToAggregate(x, interval)))
      .GroupBy(x => new
      {
        x.MeterId,
        x.MeasurementLocationId,
        x.Timestamp,
        x.Interval
      })
      .Select(x => x.Aggregate(aggregateUpserter.UpsertModelAgnostic))
      .ToList();

    AddToCacheInternal(aggregates);
    FlushCacheInternal(aggregates);

    var entities = measurements
      .Select(modelEntityConverter.ToEntity<IMeasurementEntity>)
      .ToList();

    logger.LogDebug(
      "Upserting {Count} measurements...",
      entities.Count);
    stopwatch = Stopwatch.StartNew();
    var result = await mutations.UpsertMeasurements(
      entities,
      cancellationToken
    );
    stopwatch.Stop();
    logger.LogDebug(
      "Upserted {Count} measurements in {Elapsed}",
      entities.Count,
      stopwatch.Elapsed);

    var models = result
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();

    return new UpsertMeasurementsResult(
      models,
      new()
    );
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

  public async Task<List<IAggregate>> FlushCache(
    CancellationToken cancellationToken,
    IEnumerable<IAggregate>? toStay = null
  )
  {
    var measurements = FlushCacheInternal(toStay);

    var entities = measurements
      .Select(modelEntityConverter.ToEntity<IMeasurementEntity>);

    var result = await mutations.UpsertMeasurements(
      entities,
      cancellationToken
    );

    var models = result
      .Select(modelEntityConverter.ToModel<IAggregate>)
      .ToList();

    return models;
  }

  private void AddToCacheInternal(
    IEnumerable<IAggregate> aggregates
  )
  {
    var count = 0;
    foreach (var aggregate in aggregates)
    {
      count++;
      UpsertAggregateCache
        .AddOrUpdate(
          CreateKey(aggregate),
          _ => new List<IAggregate>() { aggregate },
          (_, list) =>
          {
            list.Add(aggregate);
            return list;
          });
    }
    logger.LogDebug("Added aggregates to cache {Count}", count);
  }

  private List<IAggregate> FlushCacheInternal(
    IEnumerable<IAggregate>? toStay = null
  )
  {
    var cacheCopy = UpsertAggregateCache
      .ToDictionary(
        x => x.Key,
        x => x.Value.ToList());

    var result = new List<IAggregate>();
    if (toStay is null)
    {
      foreach (var cached in cacheCopy)
      {
        if (UpsertAggregateCache.TryRemove(cached.Key, out var value))
        {
          var upserted = value
            .Aggregate(aggregateUpserter.UpsertModelAgnostic);
          result.Add(upserted);
        }
      }
    }
    else
    {
      foreach (var cached in cacheCopy)
      {
        var upserted = cached.Value
          .Aggregate(aggregateUpserter.UpsertModelAgnostic);
        if (!toStay.Any(aggregate =>
          aggregate.MeterId == upserted.MeterId
          && aggregate.MeasurementLocationId == upserted.MeasurementLocationId
          && aggregate.Timestamp >= upserted.Timestamp
          && aggregate.Timestamp < upserted.Timestamp.Add(
            upserted.Interval.ToTimeSpan(upserted.Timestamp))
          && aggregate.Interval == IntervalModel.QuarterHour)
          && UpsertAggregateCache.TryRemove(cached.Key, out var value))
        {
          upserted = value
            .Aggregate(aggregateUpserter.UpsertModelAgnostic);
          result.Add(upserted);
        }
      }
    }

    if (result.Count is 0)
    {
      return result;
    }

    logger.LogDebug(
      "Flushing aggregate cache with {AggregateCount} aggregates",
      result.Count);
    publisher.PublishFlush(new AggregateFlushEventArgs
    {
      Aggregates = result
    });

    return result;
  }

  private static UpsertAggregateCacheKey CreateKey(
    IAggregate aggregate)
  {
    return new UpsertAggregateCacheKey(
      aggregate.MeterId,
      aggregate.MeasurementLocationId,
      aggregate.Interval,
      aggregate.Timestamp);
  }

  private static readonly ConcurrentDictionary<
    UpsertAggregateCacheKey,
    List<IAggregate>> UpsertAggregateCache =
      new ConcurrentDictionary<
        UpsertAggregateCacheKey,
        List<IAggregate>>();

  private sealed record UpsertAggregateCacheKey(
    string MeterId,
    string MeasurementLocationId,
    IntervalModel Interval,
    DateTimeOffset Timestamp
  );
}
