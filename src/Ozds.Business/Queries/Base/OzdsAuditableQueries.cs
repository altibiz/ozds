using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries.Base;

public class OzdsAuditableQueries
{
  protected readonly OzdsDbContext context;

  public OzdsAuditableQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<T?> ReadSingle<T>(string id) where T : class, IAuditable
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T))
                      as IQueryable<AuditableEntity>
                    ?? throw new InvalidOperationException();
    var item = await queryable.WithId(id).FirstOrDefaultAsync();
    return item is null ? null : EntityModelTypeMapper.ToModel<T>(item);
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    IEnumerable<string> orderByDescClauses,
    IEnumerable<string> orderByAscClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IAuditable
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var count = await filtered.CountAsync();
    var orderedByDesc = orderByDescClauses.Aggregate(filtered,
      (current, clause) => current.OrderByDescendingDynamic(clause));
    var orderedByAsc = orderByAscClauses.Aggregate(orderedByDesc,
      (current, clause) => current.OrderByDynamic(clause));
    var items = await orderedByAsc.Skip((pageNumber - 1) * pageCount)
      .Take(pageCount).ToListAsync();
    return items.Select(item => EntityModelTypeMapper.ToModel<T>(item))
      .ToPaginatedList(count);
  }
}
