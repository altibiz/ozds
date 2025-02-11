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
  public async Task<PaginatedList<IMeasurement>> ReadByMeterIdsDynamic(
    IEnumerable<IMeter> meters,
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

    var appropriateIntervalModel = QueryConstants
      .AppropriateInterval(timeSpan, fromDate);
    var appropriateInterval = appropriateIntervalModel?.ToEntity();
    var isAggregate = appropriateInterval is not null;

    var modelIdsByEntityType = meters
      .Select(meter => meter.Id)
      .GroupBy(
        id => isAggregate
          ? modelEntityConverter.EntityType(
            meterNamingConvention.AggregateTypeForMeterId(id))
          : modelEntityConverter.EntityType(
            meterNamingConvention.MeasurementTypeForMeterId(id)));

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
      .Peak()
      .Where(x =>
          (appropriateIntervalModel is not null
            ? x is IAggregate aggregate
              && aggregate.Interval == appropriateIntervalModel
            : x is not IAggregate)
          && meters.Any(meter => meter.Id == x.MeterId)
          && x.Timestamp >= fromDate
          && x.Timestamp < toDate
      )
      .ToList();

    var models = entities.Items
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .Concat(buffered)
      .ToPaginatedList(entities.TotalCount + buffered.Count);

    return models;
  }

  public async Task<List<IMeasurement>> ReadByMeterIdsLastDynamic(
    IEnumerable<IMeter> meters,
    CancellationToken cancellationToken,
    IntervalModel? interval = default,
    DateTimeOffset toDate = default
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;

    var isAggregate = interval is not null;

    var modelIdsByEntityType = meters
      .Select(meter => meter.Id)
      .GroupBy(
        id => isAggregate
          ? modelEntityConverter.EntityType(
            meterNamingConvention.AggregateTypeForMeterId(id))
          : modelEntityConverter.EntityType(
            meterNamingConvention.MeasurementTypeForMeterId(id)));

    var entities = await queries.ReadLastByMeterIds(
      modelIdsByEntityType,
      interval?.ToEntity(),
      toDate,
      cancellationToken
    );

    var buffered = measurementBuffer
      .Peak()
      .Where(x =>
          (interval is not null
            ? x is IAggregate aggregate
              && aggregate.Interval == interval
            : x is not IAggregate)
          && meters.Any(meter => meter.Id == x.MeterId)
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

  public async Task<PaginatedList<IMeasurement>>
    ReadByMeasurementLocationIdsDynamic(
      IEnumerable<IMeasurementLocation> measurementLocations,
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

    var appropriateIntervalModel = QueryConstants
      .AppropriateInterval(timeSpan, fromDate);
    var appropriateInterval = appropriateIntervalModel?.ToEntity();

    var entities = await queries.ReadByMeasurementLocationIds(
      measurementLocations.Select(x => x.Id),
      appropriateInterval,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var buffered = measurementBuffer
      .Peak()
      .Where(x =>
          (appropriateIntervalModel is not null
            ? x is IAggregate aggregate
              && aggregate.Interval == appropriateIntervalModel
            : x is not IAggregate)
          && measurementLocations
            .Any(measurementLocation =>
              measurementLocation.Id == x.MeasurementLocationId)
          && x.Timestamp >= fromDate
          && x.Timestamp < toDate
      )
      .ToList();

    var models = entities.Items
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .Concat(buffered)
      .ToPaginatedList(entities.TotalCount + buffered.Count);

    return models;
  }

  public async Task<List<IMeasurement>>
    ReadByMeasurementLocationIdsLastDynamic(
      IEnumerable<IMeasurementLocation> measurementLocations,
      CancellationToken cancellationToken,
      IntervalModel? interval = default,
      DateTimeOffset toDate = default
    )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;

    var entities = await queries.ReadLastByMeasurementLocationIds(
      measurementLocations.Select(x => x.Id),
      interval?.ToEntity(),
      toDate,
      cancellationToken
    );

    var buffered = measurementBuffer
      .Peak()
      .Where(x =>
          (interval is not null
            ? x is IAggregate aggregate
              && aggregate.Interval == interval
            : x is not IAggregate)
          && measurementLocations
            .Any(measurementLocation =>
              measurementLocation.Id == x.MeasurementLocationId)
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
