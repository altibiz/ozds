using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Base;

public class OzdsEventQueries
{
  protected readonly OzdsDbContext context;

  public OzdsEventQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<T?> ReadSingle<T>(string id) where T : class, IEvent
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var item =
      await queryable.FirstOrDefaultAsync(x => (x as EventEntity)!.Id == id);
    return item is null ? null : EntityModelTypeMapper.ToModel<T>(item);
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IEvent
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var count = await filtered.CountAsync();
    var ordered =
      filtered.OrderByDescending(x => (x as EventEntity)!.Timestamp);
    var items = await filtered.Skip((pageNumber - 1) * pageCount)
      .Take(pageCount).ToListAsync();
    return items.Select(item => EntityModelTypeMapper.ToModel<T>(item))
      .ToPaginatedList(count);
  }
}
