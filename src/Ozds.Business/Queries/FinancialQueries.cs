using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using DataFinancialQueries = Ozds.Data.Queries.FinancialQueries;

namespace Ozds.Business.Queries;

public class FinancialQueries(
  DataFinancialQueries queries,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  public async Task<PaginatedList<IFinancial>> ReadByMeasurementLocationIds(
    IEnumerable<string> measurementLocationIds,
    ResolutionModel resolution,
    int multiplier,
    int pageNumber,
    CancellationToken cancellationToken,
    DateTimeOffset fromDate = default,
    DateTimeOffset toDate = default,
    int pageCount = QueryConstants.DefaultFinancialPageCount
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;
    var timeSpan = resolution.ToTimeSpan(multiplier, toDate);
    fromDate = fromDate == default ? toDate.Subtract(timeSpan) : fromDate;

    var entities = await queries.ReadByMeasurementLocationsDynamic(
      measurementLocationIds,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel<IFinancial>)
      .ToPaginatedList(entities.TotalCount);
  }

  public async Task<PaginatedList<IFinancial>> ReadByMeterIds(
    IEnumerable<string> meterIds,
    ResolutionModel resolution,
    int multiplier,
    int pageNumber,
    CancellationToken cancellationToken,
    DateTimeOffset fromDate = default,
    DateTimeOffset toDate = default,
    int pageCount = QueryConstants.DefaultFinancialPageCount
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;
    var timeSpan = resolution.ToTimeSpan(multiplier, toDate);
    fromDate = fromDate == default ? toDate.Subtract(timeSpan) : fromDate;

    var entities = await queries.ReadByMetersDynamic(
      meterIds,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );

    return entities.Items
      .Select(modelEntityConverter.ToModel<IFinancial>)
      .ToPaginatedList(entities.TotalCount);
  }
}
