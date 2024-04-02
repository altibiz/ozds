using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries.Base;

public class OzdsInvoiceQueries
{
  protected readonly OzdsDbContext context;

  public OzdsInvoiceQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<T?> ReadSingle<T>(string id) where T : class, IInvoice
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T))
      as IQueryable<InvoiceEntity>
      ?? throw new InvalidOperationException();
    var item = await queryable.WithId(id).FirstOrDefaultAsync();
    return item is null ? null : EntityModelTypeMapper.ToModel<T>(item);
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IInvoice
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T))
      as IQueryable<InvoiceEntity>
      ?? throw new InvalidOperationException();
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var timeFiltered = filtered
      .Where(invoice => invoice.IssuedOn >= fromDate)
      .Where(invoice => invoice.IssuedOn < toDate);
    var count = await timeFiltered.CountAsync();
    var ordered = timeFiltered
      .OrderByDescending(invoice => invoice.IssuedOn);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(EntityModelTypeMapper.ToModel<T>)
      .ToPaginatedList(count);
  }
}
