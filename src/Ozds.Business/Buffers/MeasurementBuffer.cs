using System.Collections.Concurrent;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Buffers.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Buffers;

public enum MeasurementBufferBehavior
{
  Realtime,
  Buffer
}

public class MeasurementBuffer(
  AgnosticAggregateUpserter aggregateUpserter,
  AgnosticMeasurementAggregateConverter aggregateConverter,
  IMeasurementFlushPublisher measurementFlushPublisher,
  ILogger<MeasurementBuffer> logger
)
  : IBuffer
{
  private const int MaxMeasurements = 10000;

  public List<IMeasurement> Add(
    IEnumerable<IMeasurement> measurements,
    MeasurementBufferBehavior bufferBehavior = MeasurementBufferBehavior.Buffer
  )
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

    if (bufferBehavior is MeasurementBufferBehavior.Buffer)
    {
      AddToMeasurementsInternal(measurements.Where(x => x is not IAggregate));
    }
    AddToAggregatesInternal(aggregates);

    var flushedMeasurements =
      bufferBehavior is MeasurementBufferBehavior.Buffer
        ? FlushMeasurementsInternal(MaxMeasurements)
        : measurements.Where(x => x is not IAggregate).ToList();
    var flushedAggregates = FlushAggregatesInternal(aggregates);
    if (flushedMeasurements.Count is > 0 || flushedAggregates.Count is > 0)
    {
      measurementFlushPublisher.Publish(new MeasurementFlushEventArgs
      {
        Measurements = flushedMeasurements.Concat(flushedAggregates).ToList(),
      });
    }

    return flushedMeasurements
      .ToList();
  }

  public List<IMeasurement> Flush(bool immediate = false)
  {
    var measurements = FlushMeasurementsInternal();
    var aggregates = FlushAggregatesInternal();

    if (!immediate && (measurements.Count is > 0 || aggregates.Count is > 0))
    {
      measurementFlushPublisher.Publish(new MeasurementFlushEventArgs
      {
        Measurements = measurements.Concat(aggregates).ToList(),
      });
    }

    return measurements
      .Concat(aggregates)
      .ToList();
  }

  private void AddToMeasurementsInternal(
    IEnumerable<IMeasurement> measurements
  )
  {
    var count = 0;
    foreach (var measurement in measurements)
    {
      count++;
      Measurements.Enqueue(measurement);
    }
    logger.LogDebug("Added {Count} measurements to buffer", count);
  }

  public List<IMeasurement> FlushMeasurementsInternal(int? withMax = null)
  {
    var count = Measurements.Count;
    if (withMax is { } && count < withMax)
    {
      return new();
    }

    var result = new List<IMeasurement>();
    foreach (var index in Enumerable.Range(0, count))
    {
      if (Measurements.TryDequeue(out var measurement))
      {
        result.Add(measurement);
      }
    }
    logger.LogDebug("Flushed measurements buffer with {Count} measurements", result.Count);

    return result;
  }

  private void AddToAggregatesInternal(
    IEnumerable<IAggregate> aggregates
  )
  {
    var count = 0;
    foreach (var aggregate in aggregates)
    {
      count++;
      Aggregates
        .AddOrUpdate(
          CreateKey(aggregate),
          _ => new List<IAggregate>() { aggregate },
          (_, list) =>
          {
            list.Add(aggregate);
            return list;
          });
    }
    logger.LogDebug("Added {Count} aggregates to buffer", count);
  }

  private List<IAggregate> FlushAggregatesInternal(
    IEnumerable<IAggregate>? toStay = null
  )
  {
    var cacheCopy = Aggregates
      .ToDictionary(
        x => x.Key,
        x => x.Value.ToList());

    var result = new List<IAggregate>();
    if (toStay is null)
    {
      foreach (var cached in cacheCopy)
      {
        if (Aggregates.TryRemove(cached.Key, out var value))
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
          && Aggregates.TryRemove(cached.Key, out var value))
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

  private static readonly ConcurrentQueue<IMeasurement> Measurements =
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
    List<IAggregate>> Aggregates =
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
