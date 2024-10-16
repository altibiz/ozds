using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming.Agnostic;
using Ozds.Business.Queries.Abstractions;
using DataMeasurementQueries = Ozds.Data.Queries.Agnostic.MeasurementQueries;

namespace Ozds.Business.Queries.Agnostic;

public class MeasurementQueries(
  DataMeasurementQueries queries,
  AgnosticModelEntityConverter modelEntityConverter,
  AgnosticMeterNamingConvention meterNamingConvention
) : IQueries
{
  public async Task<PaginatedList<IMeasurement>> ReadDynamic(
    IEnumerable<IMeter> meters,
    ResolutionModel resolution,
    int multiplier,
    int pageNumber,
    CancellationToken cancellationToken,
    DateTimeOffset fromDate = default,
    DateTimeOffset toDate = default,
    int pageCount = QueryConstants.MeasurementPageCount
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;
    var timeSpan = resolution.ToTimeSpan(multiplier, toDate);
    fromDate = fromDate == default ? toDate.Subtract(timeSpan) : fromDate;

    var appropriateInterval = QueryConstants
      .AppropriateInterval(timeSpan, fromDate)
      ?.ToEntity();
    var isAggregate = appropriateInterval is { };

    var modelIdsByEntityType = meters
      .Select(meter => meter.Id)
      .GroupBy(id => isAggregate
        ? modelEntityConverter.EntityType(
            meterNamingConvention.AggregateTypeForMeterId(id))
        : modelEntityConverter.EntityType(
            meterNamingConvention.MeasurementTypeForMeterId(id)));

    var entities = await queries.Read(
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

  public async Task<List<IMeasurement>> ReadLastDynamic(
    IEnumerable<IMeter> meters,
    CancellationToken cancellationToken,
    IntervalModel? interval = default,
    DateTimeOffset toDate = default
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;

    var isAggregate = interval is { };

    var modelIdsByEntityType = meters
      .Select(meter => meter.Id)
      .GroupBy(id => isAggregate
        ? modelEntityConverter.EntityType(
            meterNamingConvention.AggregateTypeForMeterId(id))
        : modelEntityConverter.EntityType(
            meterNamingConvention.MeasurementTypeForMeterId(id)));

    var entities = await queries.ReadLast(
      modelIdsByEntityType,
      interval?.ToEntity(),
      toDate,
      cancellationToken
    );

    return entities
      .Select(modelEntityConverter.ToModel<IMeasurement>)
      .ToList();
  }
}
