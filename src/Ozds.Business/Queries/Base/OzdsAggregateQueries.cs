using Microsoft.EntityFrameworkCore;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Base;

public class OzdsAggregateQueries
{
  protected readonly OzdsDbContext context;

  public OzdsAggregateQueries(OzdsDbContext context)
  {
    this.context = context;
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IAggregate
  {
    var queryable = EntityModelTypeMapper.GetDbSet(context, typeof(T));
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var timeFiltered = filtered.Where(x =>
      (x as AggregateEntity)!.Timestamp >= fromDate &&
      (x as AggregateEntity)!.Timestamp < toDate);
    var count = await timeFiltered.CountAsync();
    var ordered =
      timeFiltered.OrderByDescending(x => (x as AggregateEntity)!.Timestamp);
    var items = await ordered.Skip((pageNumber - 1) * pageCount).Take(pageCount)
      .ToListAsync();
    return items.Select(item => EntityModelTypeMapper.ToModel<T>(item))
      .ToPaginatedList(count);
  }
}
