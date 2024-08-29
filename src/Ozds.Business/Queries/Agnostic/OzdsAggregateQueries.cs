using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Business.Queries.Agnostic;

public class OzdsAggregateQueries(
  DataDbContext context,
  AgnosticModelEntityConverter modelEntityConverter
) : IQueries
{
  private readonly DataDbContext _context = context;

  private readonly AgnosticModelEntityConverter _modelEntityConverter =
    modelEntityConverter;

  public async Task<PaginatedList<T>> Read<T>(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
    where T : class, IAggregate
  {
    var dbSetType = _modelEntityConverter.EntityType(typeof(T));
    var queryable = _context.GetQueryable(dbSetType)
        as IQueryable<AggregateEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");
    var filtered = whereClauses.Aggregate(
      queryable,
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

  public async Task<List<IAggregate>> ReadAgnostic(
    IEnumerable<string> whereClauses,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount)
  {
    var abbB2x = await Read<AbbB2xAggregateModel>(
      whereClauses, fromDate,
      toDate, pageNumber, pageCount);
    var schneideriEM3xxx =
      await Read<SchneideriEM3xxxAggregateModel>(
        whereClauses, fromDate, toDate,
        pageNumber, pageCount);
    return abbB2x.Items.Cast<IAggregate>().Concat(schneideriEM3xxx.Items)
      .ToList();
  }

  public async Task<PaginatedList<IAggregate>> ReadDynamic(
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    Type? aggregateType = null,
    string? meterId = null,
    IntervalModel? interval = null,
    string? whereClause = null,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultPageCount
  )
  {
    using var @lock = await mutex.LockAsync();
    var mutexContext = @lock.Context;

    if (aggregateType is null)
    {
      aggregateType = meterNamingConvention
        .AggregateTypeForLineAndMeterId(
          meterId ?? throw new ArgumentNullException(nameof(meterId)));
    }

    var dbSetType = modelEntityConverter.EntityType(aggregateType);
    var queryable = mutexContext.GetQueryable(dbSetType)
        as IQueryable<AggregateEntity>
      ?? throw new InvalidOperationException(
        $"No DbSet found for {dbSetType}");

    var whereFiltered = whereClause is null
      ? queryable
      : queryable.WhereDynamic(whereClause);

    var timeFiltered = whereFiltered
      .Where(aggregate => aggregate.Timestamp >= fromDate)
      .Where(aggregate => aggregate.Timestamp < toDate);

    var intervalEntity = interval?.ToEntity();
    var intervalFiltered = intervalEntity is null
      ? timeFiltered
      : timeFiltered.Where(aggregate => aggregate.Interval == intervalEntity);

    var filtered = intervalFiltered.Where(aggregate =>
          aggregate.MeterId == meterId);

    var ordered = filtered
      .OrderByDescending(aggregate => aggregate.Timestamp);

    var count = await filtered.CountAsync();

    var items = await ordered
      .Skip((pageNumber - 1) * pageCount)
      .Take(pageCount)
      .ToListAsync();

    return items
      .Select(modelEntityConverter.ToModel)
      .OfType<IAggregate>()
      .ToPaginatedList(count);
  }
}
