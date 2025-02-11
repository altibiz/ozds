using System.Collections.Concurrent;
using Ozds.Business.Aggregation;
using Ozds.Business.Buffers.Abstractions;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Buffers;

// TODO: only flush monthly/daily when a quarter hourly gets flushed

public enum MeasurementBufferBehavior
{
  Realtime,
  Buffer
}

public class MeasurementBuffer(
  AggregateUpserter aggregateUpserter,
  MeasurementAggregateConverter aggregateConverter,
  IMeasurementFlushPublisher measurementFlushPublisher,
  IMeasurementsBufferedPublisher measurementsBufferedPublisher,
  ILogger<MeasurementBuffer> logger
)
  : IBuffer
{
  private const int MaxMeasurements = 10000;

  private static readonly object _measurementsLock = new();

  private static readonly List<IMeasurement> Measurements = new();

  private static readonly ConcurrentDictionary<
    UpsertAggregateCacheKey,
    List<IAggregate>> Aggregates = new();

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
        .SelectMany(
          x => Enum.GetValues<IntervalModel>()
            .Select(interval => aggregateConverter.ToAggregate(x, interval)))
        .GroupBy(
          x => new
          {
            x.MeterId,
            x.MeasurementLocationId,
            x.Timestamp,
            x.Interval
          })
        .Select(x => x.Aggregate(aggregateUpserter.UpsertModel))
        .ToList();
    }

    if (bufferBehavior is MeasurementBufferBehavior.Buffer)
    {
      var nonAggregates = measurements
        .Where(x => x is not IAggregate)
        .ToList();

      AddToMeasurementsInternal(nonAggregates);

      measurementsBufferedPublisher.Publish(
        new MeasurementsBufferedEventArgs
        {
          Measurements = nonAggregates
        });
    }

    AddToAggregatesInternal(aggregates);

    var flushedMeasurements =
      bufferBehavior is MeasurementBufferBehavior.Buffer
        ? FlushMeasurementsInternal(MaxMeasurements)
        : measurements.Where(x => x is not IAggregate).ToList();
    var flushedAggregates = FlushAggregatesInternal(aggregates);
    if (flushedMeasurements.Count is > 0 || flushedAggregates.Count is > 0)
    {
      measurementFlushPublisher.Publish(
        new MeasurementFlushEventArgs
        {
          Measurements = flushedMeasurements.Concat(flushedAggregates).ToList()
        });
    }

    return flushedMeasurements
      .ToList();
  }

  public List<IMeasurement> Peak()
  {
    var measurements = PeakMeasurementsInternal();
    var aggregates = PeakAggregatesInternal();

    return measurements
      .Concat(aggregates)
      .ToList();
  }

  public List<IMeasurement> Flush(bool immediate = false)
  {
    var measurements = FlushMeasurementsInternal();
    var aggregates = FlushAggregatesInternal();

    if (!immediate && (measurements.Count is > 0 || aggregates.Count is > 0))
    {
      measurementFlushPublisher.Publish(
        new MeasurementFlushEventArgs
        {
          Measurements = measurements.Concat(aggregates).ToList()
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
    var measurementsList = measurements.ToList();
    var count = measurementsList.Count;
    var totalCount = 0;

    lock (_measurementsLock)
    {
      Measurements.AddRange(measurementsList);
      totalCount = Measurements.Count;
    }

    logger.LogDebug(
      "Added {Count} measurements to buffer now at {TotalCount}",
      count,
      totalCount
    );
  }

  public List<IMeasurement> FlushMeasurementsInternal(int? withMax = null)
  {
    var count = 0;
    lock (_measurementsLock)
    {
      count = Measurements.Count;
    }
    if (withMax is not null && count < withMax)
    {
      return new List<IMeasurement>();
    }

    List<IMeasurement>? result = null;
    lock (_measurementsLock)
    {
      result = Measurements[..count].ToList();
      Measurements.RemoveRange(0, count);
    }

    logger.LogDebug(
      "Flushed measurements buffer with {Count} measurements",
      result.Count);

    return result;
  }

  public List<IMeasurement> PeakMeasurementsInternal()
  {
    var result = new List<IMeasurement>();
    lock (_measurementsLock)
    {
      result.AddRange(Measurements);
    }

    return result;
  }

  private void AddToAggregatesInternal(
    IEnumerable<IAggregate> aggregates
  )
  {
    var count = 0;
    var aggregateGroups = aggregates
      .GroupBy(CreateKey)
      .ToList();

    foreach (var group in aggregateGroups)
    {
      var groupList = group.ToList();
      count += groupList.Count;
      Aggregates
        .AddOrUpdate(
          group.Key,
          _ => groupList,
          (_, list) =>
          {
            list.AddRange(groupList);
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
            .Aggregate(aggregateUpserter.UpsertModel);
          result.Add(upserted);
        }
      }
    }
    else
    {
      foreach (var cached in cacheCopy)
      {
        var upserted = cached.Value
          .Aggregate(aggregateUpserter.UpsertModel);
        if (!toStay.Any(
            toStayAggregate =>
              toStayAggregate.Interval == IntervalModel.QuarterHour
              && upserted.Interval == IntervalModel.QuarterHour
              && toStayAggregate.MeterId == upserted.MeterId
              && toStayAggregate.MeasurementLocationId
              == upserted.MeasurementLocationId
              && toStayAggregate.Timestamp >= upserted.Timestamp
              && toStayAggregate.Timestamp < upserted.Timestamp.Add(
                upserted.Interval.ToTimeSpan(upserted.Timestamp)))
          && Aggregates.TryRemove(cached.Key, out var value))
        {
          upserted = value
            .Aggregate(aggregateUpserter.UpsertModel);
          result.Add(upserted);
        }
      }
    }

    logger.LogDebug(
      "Flushed aggregates buffer with {Count} aggregates",
      result.Count);
    return result;
  }

  public List<IAggregate> PeakAggregatesInternal()
  {
    return Aggregates.Values.SelectMany(x => x).ToList();
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

  private sealed record UpsertAggregateCacheKey(
    string MeterId,
    string MeasurementLocationId,
    IntervalModel Interval,
    DateTimeOffset Timestamp
  );
}
