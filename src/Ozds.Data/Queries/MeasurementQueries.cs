using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Queries.Abstractions;
using Ozds.Data.Reflection;
using Z.EntityFramework.Plus;

// TODO: remove any direct references
// to concrete aggregate/measurement entity types in all queries
// NOTE: <= toDate because when getting invoice data for instance
// you wanna get the data for both the target month and the next month
// so it overlaps nicely

namespace Ozds.Data.Queries;

public class MeasurementQueries(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector
) : IQueries
{
  public async Task<PaginatedList<IMeasurementEntity>> ReadByMeterIds(
    IEnumerable<KeyValuePair<Type, IEnumerable<string>>> meterIdsByEntityType,
    IntervalEntity? interval,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    List<IMeasurementEntity> items = new();
    var count = 0;

    var futureCounts = new List<QueryDeferred<int>>();
    var futureItems = new List<QueryFutureEnumerable<IMeasurementEntity>>();

    foreach (var group in meterIdsByEntityType)
    {
      var entityType = group.Key;
      var meterIds = group.Value;
      var queryable = context.GetQueryable<IMeasurementEntity>(entityType);

      var filtered = queryable
        .Where(measurement => measurement.Timestamp >= fromDate)
        .Where(measurement => measurement.Timestamp <= toDate);

      if (entityType.IsAssignableTo(typeof(IAggregateEntity)))
      {
        var intervalParameter = Expression.Parameter(
          typeof(IMeasurementEntity),
          "entity"
        );
        var intervalExpression = Expression.Lambda<
          Func<IMeasurementEntity, bool>
        >(
          Expression.Equal(
            Expression.Property(
              Expression.Convert(intervalParameter, typeof(IAggregateEntity)),
              nameof(AggregateEntity<MeterEntity>.Interval)
            ),
            Expression.Constant(
              interval
                ?? throw new InvalidOperationException("Interval is null"),
              typeof(IntervalEntity)
            )
          ),
          intervalParameter
        );

        filtered = filtered.Where(intervalExpression);
      }

      var foreignKeyParameter = Expression.Parameter(
        typeof(IMeasurementEntity),
        "entity"
      );
      var foreignKeyExpression = Expression.Lambda<
        Func<IMeasurementEntity, bool>
      >(
        Expression.Invoke(
          context.ForeignKeyIn(
            entityType,
            nameof(MeasurementEntity<MeterEntity>.Meter),
            meterIds
          ),
          Expression.Convert(foreignKeyParameter, typeof(object))
        ),
        foreignKeyParameter
      );
      filtered = filtered.Where(foreignKeyExpression);

      var ordered = filtered.OrderBy(measurement => measurement.Timestamp);

      var paged = ordered.Skip(pageNumber * pageCount).Take(pageCount);

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

    return new PaginatedList<IMeasurementEntity>(items, count);
  }

  public async Task<List<IMeasurementEntity>> ReadLastByMeterIds(
    IEnumerable<IGrouping<Type, string>> meterIdsByEntityType,
    IntervalEntity? interval,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    List<IMeasurementEntity> items = new();

    var futureItems = new List<QueryDeferred<IMeasurementEntity>>();

    foreach (var group in meterIdsByEntityType)
    {
      var entityType = group.Key;
      var meterIds = group;
      var queryable = context.GetQueryable<IMeasurementEntity>(entityType);

      var filtered = queryable.Where(measurement =>
        measurement.Timestamp <= toDate
      );

      if (entityType.IsAssignableTo(typeof(IAggregateEntity)))
      {
        var intervalParameter = Expression.Parameter(
          typeof(IMeasurementEntity),
          "entity"
        );
        var intervalExpression = Expression.Lambda<
          Func<IMeasurementEntity, bool>
        >(
          Expression.Equal(
            Expression.Property(
              Expression.Convert(intervalParameter, typeof(IAggregateEntity)),
              nameof(AggregateEntity<MeterEntity>.Interval)
            ),
            Expression.Constant(
              interval
                ?? throw new InvalidOperationException("Interval is null"),
              typeof(IntervalEntity)
            )
          ),
          intervalParameter
        );

        filtered = filtered.Where(intervalExpression);
      }

      var foreignKeyParameter = Expression.Parameter(
        typeof(IMeasurementEntity),
        "entity"
      );
      var foreignKeyExpression = Expression.Lambda<
        Func<IMeasurementEntity, bool>
      >(
        Expression.Invoke(
          context.ForeignKeyIn(
            entityType,
            nameof(MeasurementEntity<MeterEntity>.Meter),
            meterIds
          ),
          Expression.Convert(foreignKeyParameter, typeof(object))
        ),
        foreignKeyParameter
      );
      filtered = filtered.Where(foreignKeyExpression);

      var ordered = filtered.OrderByDescending(measurement =>
        measurement.Timestamp
      );

      futureItems.Add(ordered.DeferredLastOrDefault());
    }

    foreach (var futureCount in futureItems)
    {
      items.Add(await futureCount.FutureValue().ValueAsync(cancellationToken));
    }

    return items;
  }

  public async Task<
    PaginatedList<IMeasurementEntity>
  > ReadByMeasurementLocationIds(
    IEnumerable<string> measurementLocationIds,
    IntervalEntity? interval,
    DateTimeOffset fromDate,
    DateTimeOffset toDate,
    int pageNumber,
    CancellationToken cancellationToken,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    List<IMeasurementEntity> items = new();
    var count = 0;

    var futureCounts = new List<QueryDeferred<int>>();
    var futureItems = new List<QueryFutureEnumerable<IMeasurementEntity>>();

    var types = interval is not null
      ? reflector.AggregateTypes
      : reflector.MeasurementTypes;

    foreach (var entityType in types)
    {
      var queryable = context.GetQueryable<IMeasurementEntity>(entityType);

      var filtered = queryable
        .Where(measurement => measurement.Timestamp >= fromDate)
        .Where(measurement => measurement.Timestamp <= toDate);

      if (entityType.IsAssignableTo(typeof(IAggregateEntity)))
      {
        var intervalParameter = Expression.Parameter(
          typeof(IMeasurementEntity),
          "entity"
        );
        var intervalExpression = Expression.Lambda<
          Func<IMeasurementEntity, bool>
        >(
          Expression.Equal(
            Expression.Property(
              Expression.Convert(intervalParameter, typeof(IAggregateEntity)),
              nameof(AggregateEntity<MeterEntity>.Interval)
            ),
            Expression.Constant(
              interval
                ?? throw new InvalidOperationException("Interval is null"),
              typeof(IntervalEntity)
            )
          ),
          intervalParameter
        );

        filtered = filtered.Where(intervalExpression);
      }

      var foreignKeyParameter = Expression.Parameter(
        typeof(IMeasurementEntity),
        "entity"
      );
      var foreignKeyExpression = Expression.Lambda<
        Func<IMeasurementEntity, bool>
      >(
        Expression.Invoke(
          context.ForeignKeyIn(
            entityType,
            nameof(MeasurementEntity<MeterEntity>.MeasurementLocation),
            measurementLocationIds
          ),
          Expression.Convert(foreignKeyParameter, typeof(object))
        ),
        foreignKeyParameter
      );
      filtered = filtered.Where(foreignKeyExpression);

      var ordered = filtered.OrderBy(measurement => measurement.Timestamp);

      var paged = ordered.Skip(pageNumber * pageCount).Take(pageCount);

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

    return new PaginatedList<IMeasurementEntity>(items, count);
  }

  public async Task<List<IMeasurementEntity>> ReadLastByMeasurementLocationIds(
    IEnumerable<string> measurementLocationIds,
    IntervalEntity? interval,
    DateTimeOffset toDate,
    CancellationToken cancellationToken
  )
  {
    await using var context = await factory.CreateDbContextAsync(
      cancellationToken
    );

    List<IMeasurementEntity> items = new();

    var futureItems = new List<QueryFutureEnumerable<IMeasurementEntity>>();

    var types = interval is not null
      ? reflector.AggregateTypes
      : reflector.MeasurementTypes;

    foreach (var entityType in types)
    {
      var queryable = context.GetQueryable<IMeasurementEntity>(entityType);

      var filtered = queryable.Where(measurement =>
        measurement.Timestamp <= toDate
      );

      if (entityType.IsAssignableTo(typeof(IAggregateEntity)))
      {
        var intervalParameter = Expression.Parameter(
          typeof(IMeasurementEntity),
          "entity"
        );
        var intervalExpression = Expression.Lambda<
          Func<IMeasurementEntity, bool>
        >(
          Expression.Equal(
            Expression.Property(
              Expression.Convert(intervalParameter, typeof(IAggregateEntity)),
              nameof(AggregateEntity<MeterEntity>.Interval)
            ),
            Expression.Constant(
              interval
                ?? throw new InvalidOperationException("Interval is null"),
              typeof(IntervalEntity)
            )
          ),
          intervalParameter
        );

        filtered = filtered.Where(intervalExpression);
      }

      var foreignKeyParameter = Expression.Parameter(
        typeof(IMeasurementEntity),
        "entity"
      );
      var foreignKeyExpression = Expression.Lambda<
        Func<IMeasurementEntity, bool>
      >(
        Expression.Invoke(
          context.ForeignKeyIn(
            entityType,
            nameof(MeasurementEntity<MeterEntity>.MeasurementLocation),
            measurementLocationIds
          ),
          Expression.Convert(foreignKeyParameter, typeof(object))
        ),
        foreignKeyParameter
      );
      filtered = filtered.Where(foreignKeyExpression);

      var lastTimestampByMeter = filtered
        .GroupBy(m => m.MeterId)
        .Select(g => new
        {
          MeterId = g.Key,
          Timestamp = g.Max(x => x.Timestamp),
        });

      var lastByEveryMeter = filtered.Join(
        lastTimestampByMeter,
        m => new { m.MeterId, m.Timestamp },
        x => new { x.MeterId, x.Timestamp },
        (m, _) => m
      );

      futureItems.Add(lastByEveryMeter.Future()!);
    }

    foreach (var futureItem in futureItems)
    {
      items.AddRange(await futureItem.ToListAsync(cancellationToken));
    }

    return items;
  }
}
