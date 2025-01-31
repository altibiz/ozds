using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class FinancialQueries(
  IDbContextFactory<DataDbContext> factory
)
{
  public async Task<PaginatedList<IFinancialEntity>>
    ReadByMeasurementLocationsDynamic(
      IEnumerable<string> measurementLocationIds,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultFinancialPageCount
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.NetworkUserCalculations
      .Where(
        context.ForeignKeyIn<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.NetworkUserMeasurementLocationId),
          measurementLocationIds))
      .Where(calculation => calculation.FromDate >= fromDate)
      .Where(calculation => calculation.FromDate < toDate)
      .Include(calculation => calculation.NetworkUserInvoice);

    var ordered = filtered
      .OrderByDescending(calculation => calculation.IssuedOn);

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return new PaginatedList<IFinancialEntity>(
      items
        .Concat(
          items
            .Select(calculation => calculation.NetworkUserInvoice)
            .OfType<IFinancialEntity>()
            .DistinctBy(invoice => invoice.Id))
        .ToList(),
      count
    );
  }

  public async Task<PaginatedList<IFinancialEntity>>
    ReadByMetersDynamic(
      IEnumerable<string> meterIds,
      DateTimeOffset fromDate,
      DateTimeOffset toDate,
      int pageNumber,
      CancellationToken cancellationToken,
      int pageCount = QueryConstants.DefaultFinancialPageCount
    )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var filtered = context.NetworkUserCalculations
      .Where(
        context.ForeignKeyIn<NetworkUserCalculationEntity>(
          nameof(NetworkUserCalculationEntity.MeterId),
          meterIds))
      .Where(calculation => calculation.FromDate >= fromDate)
      .Where(calculation => calculation.FromDate < toDate)
      .Include(calculation => calculation.NetworkUserInvoice);

    var ordered = filtered
      .OrderByDescending(
        calculation => calculation.NetworkUserInvoice.IssuedOn);

    var count = await filtered.CountAsync(cancellationToken);

    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return new PaginatedList<IFinancialEntity>(
      items
        .Concat(
          items
            .Select(calculation => calculation.NetworkUserInvoice)
            .OfType<IFinancialEntity>()
            .DistinctBy(invoice => invoice.Id))
        .ToList(),
      count
    );
  }
}
