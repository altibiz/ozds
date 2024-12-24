using System.Collections.Concurrent;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Buffers.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Buffers;

public class MeasurementUpsertBuffer(
  AgnosticAggregateUpserter aggregateUpserter,
  AgnosticMeasurementAggregateConverter aggregateConverter,
  IMeasurementFlushPublisher measurementFlushPublisher,
  ILogger<MeasurementUpsertBuffer> logger
)
  : IBuffer
{
  private const int MaxMeasurements = 10000;

  public List<IMeasurement> Add(IEnumerable<IMeasurement> measurements)
  {
    var aggregates = measurements
      .OfType<IAggregate>()
      .ToList();
    var initialAggregateCount = aggregates.Count;
    if (initialAggregateCount is 0)
    {
      aggregates = measurements
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
    }

    AddToMeasurementCacheInternal(measurements.Where(x => x is not IAggregate));
    AddToAggregateCacheInternal(aggregates);

    var flushedMeasurements = FlushMeasurementCacheInternal(MaxMeasurements);
    var flushedAggregates = FlushAggregateCacheInternal(aggregates);
    if (flushedMeasurements.Count is > 0 || flushedAggregates.Count is > 0)
    {
      measurementFlushPublisher.PublishFlush(new MeasurementFlushEventArgs
      {
        Measurements = flushedMeasurements.Concat(flushedAggregates).ToList(),
      });
    }

    return flushedMeasurements
      .ToList();
  }

  public List<IMeasurement> Flush()
  {
    var measurements = FlushMeasurementCacheInternal();
    var aggregates = FlushAggregateCacheInternal();

    if (measurements.Count is > 0 || aggregates.Count is > 0)
    {
      measurementFlushPublisher.PublishFlush(new MeasurementFlushEventArgs
      {
        Measurements = measurements.Concat(aggregates).ToList(),
      });
    }

    return measurements
      .Concat(aggregates)
      .ToList();
  }

  private void AddToMeasurementCacheInternal(
    IEnumerable<IMeasurement> measurements
  )
  {
    var count = 0;
    foreach (var measurement in measurements)
    {
      count++;
      MeasurementCache.Enqueue(measurement);
    }
    logger.LogDebug("Added measurements to cache {Count}", count);
  }

  public List<IMeasurement> FlushMeasurementCacheInternal(int? withMax = null)
  {
    var count = MeasurementCache.Count;
    if (withMax is { } && count < withMax)
    {
      return new();
    }

    var result = new List<IMeasurement>();
    foreach (var index in Enumerable.Range(0, count))
    {
      if (MeasurementCache.TryDequeue(out var measurement))
      {
        result.Add(measurement);
      }
    }
    logger.LogDebug("Flushed measurements cache with {Count} measurements", result.Count);

    return result;
  }

  private void AddToAggregateCacheInternal(
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

  private List<IAggregate> FlushAggregateCacheInternal(
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
    logger.LogDebug("Flushed aggregates cache with {Count} aggregates", result.Count);
    return result;
  }

  private static readonly ConcurrentQueue<IMeasurement> MeasurementCache =
    new ConcurrentQueue<IMeasurement>();

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
