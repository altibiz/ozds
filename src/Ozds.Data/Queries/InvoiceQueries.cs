using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;

namespace Ozds.Data.Queries;

public class InvoiceQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<NetworkUserInvoiceEntity>>
    ReadInvoicesByRepresentative(
      string representativeId,
      RoleEntity role,
      int pageNumber,
      CancellationToken cancellationToken,
      DateTimeOffset? fromDate = null,
      DateTimeOffset? toDate = null,
      int pageCount = QueryConstants.DefaultPageCount
    )
  {
    await using var context = await factory.CreateDbContextAsync();

    var filtered = role switch
    {
      RoleEntity.LocationRepresentative
        or RoleEntity.NetworkUserRepresentative =>
        context.Representatives
          .Where(
            context.PrimaryKeyEquals<RepresentativeEntity>(
              representativeId))
          .Include(x => x.Locations)
          .ThenInclude(x => x.NetworkUsers)
          .ThenInclude(x => x.Invoices)
          .Include(x => x.NetworkUsers)
          .ThenInclude(x => x.Invoices)
          .SelectMany(
            x => x.Locations
              .SelectMany(
                x => x.NetworkUsers
                  .SelectMany(x => x.Invoices))
              .Concat(
                x.NetworkUsers
                  .SelectMany(x => x.Invoices))),
      RoleEntity.OperatorRepresentative => context.NetworkUserInvoices,
      _ => throw new ArgumentOutOfRangeException(nameof(role))
    };

    if (fromDate is not null)
    {
      filtered = filtered.Where(x => x.FromDate >= fromDate);
    }

    if (toDate is not null)
    {
      filtered = filtered.Where(x => x.ToDate <= toDate);
    }

    var ordered = filtered.OrderByDescending(x => x.ToDate);

    var count = await ordered.CountAsync(cancellationToken);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync(cancellationToken);

    return items
      .OfType<NetworkUserInvoiceEntity>()
      .ToPaginatedList(count);
  }
}
