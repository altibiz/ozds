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
  MeterNamingConvention meterNamingConvention
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

    var appropriateInterval = QueryConstants
      .AppropriateInterval(timeSpan, fromDate)
      ?.ToEntity();
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

    var models = entities.Items
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToPaginatedList(entities.TotalCount);

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

    var entities = await queries.ReadLastByMeterId(
      modelIdsByEntityType,
      interval?.ToEntity(),
      toDate,
      cancellationToken
    );

    return entities
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();
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

    var appropriateInterval = QueryConstants
      .AppropriateInterval(timeSpan, fromDate)
      ?.ToEntity();

    var entities = await queries.ReadByMeasurementLocationIds(
      measurementLocations.Select(x => x.Id),
      appropriateInterval,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );

    var models = entities.Items
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToPaginatedList(entities.TotalCount);

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

    return entities
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();
  }
}
