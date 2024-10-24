using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Z.EntityFramework.Plus;

namespace Ozds.Data.Queries.Agnostic;

public class MeasurementQueries(
  IDbContextFactory<DataDbContext> factory
) : IQueries
{
  public async Task<PaginatedList<IMeasurementEntity>> Read(
    IEnumerable<IGrouping<Type, string>> meterIdsByEntityType,
    IntervalEntity? interval,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    List<IMeasurementEntity> items = new();
    int count = 0;

    var futureCounts = new List<QueryDeferred<int>>();
    var futureItems = new List<QueryFutureEnumerable<IMeasurementEntity>>();

    foreach (var group in meterIdsByEntityType)
    {
      var entityType = group.Key;
      var meterIds = group;
      var queryable = context.GetQueryable<IMeasurementEntity>(entityType);

      var filtered = queryable
        .Where(measurement => measurement.Timestamp >= fromDate)
        .Where(measurement => measurement.Timestamp < toDate);

      if (entityType.IsAssignableTo(typeof(IAggregateEntity)))
      {
        var intervalParameter = Expression
          .Parameter(typeof(IMeasurementEntity), "entity");
        var intervalExpression = Expression
          .Lambda<Func<IMeasurementEntity, bool>>(
            Expression.Equal(
              Expression.Property(
                intervalParameter,
                nameof(AggregateEntity<MeterEntity>.Interval)),
              Expression.Constant(interval)),
            intervalParameter);

        filtered = filtered.Where(intervalExpression);
      }

      var foreignKeyParameter = Expression
        .Parameter(typeof(IMeasurementEntity), "entity");
      var foreignKeyExpression = Expression
        .Lambda<Func<IMeasurementEntity, bool>>(
          Expression.Invoke(
            context.ForeignKeyIn(
              entityType,
              nameof(MeasurementEntity<MeterEntity>.Meter),
              meterIds),
            Expression.Convert(foreignKeyParameter, typeof(object))),
          foreignKeyParameter);
      filtered = filtered.Where(foreignKeyExpression);

      var ordered = filtered
        .OrderBy(measurement => measurement.Timestamp);

      var paged = ordered
        .Skip((pageNumber - 1) * pageCount)
        .Take(pageCount);

      futureCounts.Add(filtered.DeferredCount());
      futureItems.Add(paged.Future());
    }

    foreach (var futureCount in futureCounts)
    {
      count += await futureCount.FutureValue().ValueAsync(cancellationToken);
    }

    foreach (var futureItem in futureItems)
    {
      items.AddRange(await futureItem.ToListAsync(cancellationToken));
    }

    return new PaginatedList<IMeasurementEntity>(
      items,
      count
    );
  }

  public async Task<List<IMeasurementEntity>> ReadLast(
    IEnumerable<IGrouping<Type, string>> meterIdsByEntityType,
    IntervalEntity? interval,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    List<IMeasurementEntity> items = new();

    var futureItems = new List<QueryDeferred<IMeasurementEntity>>();

    foreach (var group in meterIdsByEntityType)
    {
      var entityType = group.Key;
      var meterIds = group;
      var queryable = context.GetQueryable<IMeasurementEntity>(entityType);

      var filtered = queryable
        .Where(measurement => measurement.Timestamp < toDate);

      if (entityType.IsAssignableTo(typeof(IAggregateEntity)))
      {
        var intervalParameter = Expression
          .Parameter(typeof(IMeasurementEntity), "entity");
        var intervalExpression = Expression
          .Lambda<Func<IMeasurementEntity, bool>>(
            Expression.Equal(
              Expression.Property(
                intervalParameter,
                nameof(AggregateEntity<MeterEntity>.Interval)),
              Expression.Constant(interval)),
            intervalParameter);

        filtered = filtered.Where(intervalExpression);
      }

      var foreignKeyParameter = Expression
        .Parameter(typeof(IMeasurementEntity), "entity");
      var foreignKeyExpression = Expression
        .Lambda<Func<IMeasurementEntity, bool>>(
          Expression.Invoke(
            context.ForeignKeyIn(
              entityType,
              nameof(MeasurementEntity<MeterEntity>.Meter),
              meterIds),
            Expression.Convert(foreignKeyParameter, typeof(object))),
          foreignKeyParameter);
      filtered = filtered.Where(foreignKeyExpression);

      var ordered = filtered
        .OrderByDescending(measurement => measurement.Timestamp);

      futureItems.Add(ordered.DeferredLastOrDefault());
    }

    foreach (var futureCount in futureItems)
    {
      items.Add(await futureCount.FutureValue().ValueAsync(cancellationToken));
    }

    return items;
  }
}