using Ozds.Business.Buffers;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming;
using Ozds.Business.Queries.Abstractions;
using DataMeasurementQueries = Ozds.Data.Queries.MeasurementQueries;

namespace Ozds.Business.Queries;

public class MeasurementQueries(
  DataMeasurementQueries queries,
  ModelEntityConverter modelEntityConverter,
  MeterNamingConvention meterNamingConvention,
  MeasurementBuffer measurementBuffer
) : IQueries
{
  public async Task<PaginatedList<IMeasurement>> ReadByMeterIds(
    IEnumerable<string> meterIds,
    ResolutionModel resolution,
    int multiplier,
    int pageNumber,
    CancellationToken cancellationToken,
    IntervalModel? interval = default,
    DateTimeOffset fromDate = default,
    DateTimeOffset toDate = default,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;
    var timeSpan = resolution.ToTimeSpan(multiplier, toDate);
    fromDate = fromDate == default ? toDate.Subtract(timeSpan) : fromDate;

    var appropriateIntervalModel = QueryConstants.AppropriateInterval(
      timeSpan,
      fromDate
    );

    return await ReadByMeterIds(
      meterIds,
      appropriateIntervalModel,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public async Task<PaginatedList<IMeasurement>> ReadByMeterIds(
    IEnumerable<string> meterIds,
    IntervalModel? appropriateIntervalModel,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    var appropriateInterval = appropriateIntervalModel?.ToEntity();
    var isAggregate = appropriateInterval is not null;

    var modelIdsByEntityType = meterIds.GroupBy(
      id =>
        isAggregate
          ? modelEntityConverter.EntityType(
            meterNamingConvention.AggregateTypeForMeterId(id)
          )
          : modelEntityConverter.EntityType(
            meterNamingConvention.MeasurementTypeForMeterId(id)
          )
    );

    var entities = await queries.ReadByMeterIds(
      modelIdsByEntityType,
      appropriateInterval,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var buffered = measurementBuffer
      .Peek()
      .Where(
        x =>
          (
            appropriateIntervalModel is not null
              ? x is IAggregate aggregate
              && aggregate.Interval == appropriateIntervalModel
              : x is not IAggregate
          )
          && meterIds.Any(y => y == x.MeterId)
          && x.Timestamp >= fromDate
          && x.Timestamp < toDate
      )
      .ToList();

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<IMeasurement>)
      .Concat(buffered)
      .ToPaginatedList(entities.TotalCount + buffered.Count);

    return models;
  }

  public async Task<List<IMeasurement>> ReadByMeterIdsLast(
    IEnumerable<string> meterIds,
    CancellationToken cancellationToken,
    IntervalModel? interval = default,
    DateTimeOffset toDate = default
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;

    var isAggregate = interval is not null;

    var modelIdsByEntityType = meterIds.GroupBy(
      id =>
        isAggregate
          ? modelEntityConverter.EntityType(
            meterNamingConvention.AggregateTypeForMeterId(id)
          )
          : modelEntityConverter.EntityType(
            meterNamingConvention.MeasurementTypeForMeterId(id)
          )
    );

    var entities = await queries.ReadLastByMeterIds(
      modelIdsByEntityType,
      interval?.ToEntity(),
      toDate,
      cancellationToken
    );

    var buffered = measurementBuffer
      .Peek()
      .Where(
        x =>
          (
            interval is not null
              ? x is IAggregate aggregate && aggregate.Interval == interval
              : x is not IAggregate
          )
          && meterIds.Any(y => y == x.MeterId)
          && x.Timestamp < toDate
      )
      .ToList();

    var last = entities
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .Concat(buffered)
      .GroupBy(x => x.MeterId)
      .Select(x => x.Last())
      .ToList();

    return last;
  }

  public async Task<PaginatedList<IMeasurement>> ReadByMeasurementLocationIds(
    IEnumerable<string> measurementLocationIds,
    ResolutionModel resolution,
    int multiplier,
    int pageNumber,
    CancellationToken cancellationToken,
    DateTimeOffset fromDate = default,
    DateTimeOffset toDate = default,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;
    var timeSpan = resolution.ToTimeSpan(multiplier, toDate);
    fromDate = fromDate == default ? toDate.Subtract(timeSpan) : fromDate;

    var appropriateIntervalModel = QueryConstants.AppropriateInterval(
      timeSpan,
      fromDate
    );

    return await ReadByMeasurementLocationIds(
      measurementLocationIds,
      appropriateIntervalModel,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public async Task<PaginatedList<IMeasurement>> ReadByMeasurementLocationIds(
    IEnumerable<string> measurementLocationIds,
    IntervalModel? appropriateIntervalModel,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    var entities = await queries.ReadByMeasurementLocationIds(
      measurementLocationIds,
      appropriateIntervalModel?.ToEntity(),
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var buffered = measurementBuffer
      .Peek()
      .Where(
        x =>
          (
            appropriateIntervalModel is not null
              ? x is IAggregate aggregate
              && aggregate.Interval == appropriateIntervalModel
              : x is not IAggregate
          )
          && measurementLocationIds.Any(y => y == x.MeasurementLocationId)
          && x.Timestamp >= fromDate
          && x.Timestamp < toDate
      )
      .ToList();

    var models = entities
      .Items.Select(modelEntityConverter.ToModel<IMeasurement>)
      .Concat(buffered)
      .ToPaginatedList(entities.TotalCount + buffered.Count);

    return models;
  }

  public async Task<List<IMeasurement>> ReadByMeasurementLocationIdsLast(
    IEnumerable<string> measurementLocationIds,
    CancellationToken cancellationToken,
    IntervalModel? interval = default,
    DateTimeOffset toDate = default
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;

    var entities = await queries.ReadLastByMeasurementLocationIds(
      measurementLocationIds,
      interval?.ToEntity(),
      toDate,
      cancellationToken
    );

    var buffered = measurementBuffer
      .Peek()
      .Where(
        x =>
          (
            interval is not null
              ? x is IAggregate aggregate && aggregate.Interval == interval
              : x is not IAggregate
          )
          && measurementLocationIds.Any(y => y == x.MeasurementLocationId)
          && x.Timestamp < toDate
      )
      .ToList();

    var last = entities
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .Concat(buffered)
      .GroupBy(x => x.MeterId)
      .Select(x => x.Last())
      .ToList();

    return last;
  }
}
