using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Agnotic;

public class OzdsMeasurementQueries : IOzdsQueries
{
  private readonly OzdsDbContext _context;

  private readonly IServiceProvider _serviceProvider;

  public OzdsMeasurementQueries(
    OzdsDbContext context,
    IServiceProvider serviceProvider
  )
  {
    _context = context;
    _serviceProvider = serviceProvider;
  }

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  ) where T : class, IMeasurement
  {
    var modelEntityConverter = _serviceProvider
      .GetServices<IModelEntityConverter>()
      .FirstOrDefault(converter => converter
        .CanConvertToModel(typeof(T)));
    if (modelEntityConverter is null)
    {
      throw new InvalidOperationException(
        $"No model entity converter found for {typeof(T)}");
    }

    var queryable = _context.GetDbSet(typeof(T)) as IQueryable<MeasurementEntity>
                    ?? throw new InvalidOperationException();
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
      .Select(modelEntityConverter.ToModel)
      .OfType<T>()
      .ToPaginatedList(count);
  }
}
