using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Agnotic;

public class OzdsMeasurementQueries : IOzdsQueries
{
  private readonly OzdsDbContext _context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter;

  public OzdsMeasurementQueries(
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
  ) where T : class, IMeasurement
  {
    var dbSetType = _modelEntityConverter.DbSetType(typeof(T));
    var queryable =
      _context.GetDbSet(dbSetType) as IQueryable<MeasurementEntity>
      ?? throw new InvalidOperationException("fjkdasl;fjsa");
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

  public async Task<List<IMeasurement>> ReadAgnostic(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount)
  {
    var abbB2x = await Read<AbbB2xMeasurementModel>(whereClauses, fromDate, toDate, pageNumber, pageCount);
    var schneideriEM3xxx = await Read<SchneideriEM3xxxMeasurementModel>(whereClauses, fromDate, toDate, pageNumber, pageCount);
    return abbB2x.Items.Cast<IMeasurement>().Concat(schneideriEM3xxx.Items).ToList();
  }
}
