using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Extensions;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsMeasurementQueries(
  OzdsDataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IOzdsQueries
{
  private readonly OzdsDataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<Expression<Func<MeasurementEntity, bool>>> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IMeasurement
  {
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable =
      _context.GetDbSet(dbSetType) as IQueryable<MeasurementEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");
    var filtered = whereClauses.Aggregate(
      queryable,
      (current, clause) => current.Where(clause));
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
    IEnumerable<Expression<Func<MeasurementEntity, bool>>> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount)
  {
    var abbB2x = await Read<AbbB2xMeasurementModel>(
      whereClauses, fromDate,
      toDate, pageNumber, pageCount);
    var schneideriEM3xxx =
      await Read<SchneideriEM3xxxMeasurementModel>(
        whereClauses, fromDate,
        toDate, pageNumber, pageCount);
    return abbB2x.Items.Cast<IMeasurement>().Concat(schneideriEM3xxx.Items)
      .ToList();
  }
}
