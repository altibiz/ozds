using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

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
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var item = await queryable.FirstOrDefaultAsync(x => (x as InvoiceEntity)!.Id == id);
    return item is null ? null : EntityModelTypeMapper.ToModel<T>(item);
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    IEnumerable<string> orderByDescClauses,
    IEnumerable<string> orderByAscClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IEvent
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var filtered = whereClauses.Aggregate(queryable, (current, clause) => current.WhereDynamic(clause));
    var count = await filtered.CountAsync();
    var orderedByDesc = orderByDescClauses.Aggregate(filtered, (current, clause) => current.OrderByDescendingDynamic(clause));
    var orderedByAsc = orderByAscClauses.Aggregate(orderedByDesc, (current, clause) => current.OrderByDynamic(clause));
    var items = await orderedByAsc.Skip((pageNumber - 1) * pageCount).Take(pageCount).ToListAsync();
    return items.Select(item => EntityModelTypeMapper.ToModel<T>(item)).ToPaginatedList(count);
  }
}

