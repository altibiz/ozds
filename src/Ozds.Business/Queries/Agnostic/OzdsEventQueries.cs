using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Agnotic;

public class OzdsEventQueries : IOzdsQueries
{
  private readonly OzdsDbContext _context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter;

  public OzdsEventQueries(
    OzdsDbContext context,
    AgnosticModelEntityConverter modelEntityConverter
  )
  {
    _context = context;
    _modelEntityConverter = modelEntityConverter;
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IEvent
  {
    var queryable = _context.GetDbSet(typeof(T))
      as IQueryable<EventEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {typeof(T)}");
    var filtered = whereClauses.Aggregate(queryable,
      (current, clause) => current.WhereDynamic(clause));
    var timeFiltered = filtered
      .Where(aggregate => aggregate.Timestamp >= fromDate)
      .Where(aggregate => aggregate.Timestamp < toDate);
    var count = await timeFiltered.CountAsync();
    var ordered = timeFiltered
      .OrderByDescending(aggregate => aggregate.Timestamp);
    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();
    return items
      .Select(_modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}
