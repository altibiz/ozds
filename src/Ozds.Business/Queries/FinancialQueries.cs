using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using DataFinancialQueries = Ozds.Data.Queries.FinancialQueries;

namespace Ozds.Business.Queries;

public class FinancialQueries(
  DataFinancialQueries queries,
  ModelEntityConverter modelEntityConverter
) : IQueries
{
  public Task<PaginatedList<IFinancial>> ReadByMeasurementLocationIds(
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

    return ReadByMeasurementLocationIds(
      measurementLocationIds,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public async Task<PaginatedList<IFinancial>> ReadByMeasurementLocationIds(
    IEnumerable<string> measurementLocationIds,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultFinancialPageCount
  )
  {
    var entities = await queries.ReadByMeasurementLocationIds(
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

  public Task<PaginatedList<IFinancial>> ReadByMeterIds(
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

    return ReadByMeterIds(
      meterIds,
      fromDate,
      toDate,
      pageNumber,
      cancellationToken,
      pageCount
    );
  }

  public async Task<PaginatedList<IFinancial>> ReadByMeterIds(
    IEnumerable<string> meterIds,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultFinancialPageCount
  )
  {
    var entities = await queries.ReadByMeterIds(
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
