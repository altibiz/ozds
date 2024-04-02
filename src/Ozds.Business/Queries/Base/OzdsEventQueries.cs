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

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IEvent
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T))
      as IQueryable<EventEntity>
      ?? throw new InvalidOperationException();
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var timeFiltered = filtered
      .Where(@event => @event.Timestamp >= fromDate)
      .Where(@event => @event.Timestamp < toDate);
    var count = await timeFiltered.CountAsync();
    var ordered = timeFiltered
      .OrderByDescending(@event => @event.Timestamp);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(EntityModelTypeMapper.ToModel<T>)
      .ToPaginatedList(count);
  }
}
