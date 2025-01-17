using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Naming.Agnostic;
using Ozds.Business.Queries.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Z.EntityFramework.Plus;

namespace Ozds.Business.Queries;

public class MeterGraphQueries(
  IDbContextFactory<DataDbContext> factory,
  ModelEntityConverter modelEntityConverter,
  MeterNamingConvention meterNamingConvention
) : IQueries
{
  public async Task<PaginatedList<IMeasurement>> Read(
    IEnumerable<IMeter> meters,
    ResolutionModel resolution,
    int multiplier,
    DateTimeOffset fromDate = default,
    DateTimeOffset toDate = default,
    int pageNumber = QueryConstants.StartingPage,
    int pageCount = QueryConstants.DefaultMeasurementPageCount
  )
  {
    var now = DateTimeOffset.UtcNow;
    toDate = toDate == default ? now : toDate;
    var timeSpan = resolution.ToTimeSpan(multiplier, toDate);
    fromDate = fromDate == default ? toDate.Subtract(timeSpan) : fromDate;

    var appropriateInterval = QueryConstants
      .AppropriateInterval(timeSpan, fromDate)
      ?.ToEntity();
    var isAggregate = appropriateInterval is { };

    var modelTypes = meters
      .GroupBy(meter => isAggregate
        ? meterNamingConvention.AggregateTypeForMeterId(meter.Id)
        : meterNamingConvention.MeasurementTypeForMeterId(meter.Id));

    await using var context = await factory.CreateDbContextAsync();

    List<IMeasurement> items = new();
    int count = 0;
    if (isAggregate)
    {
      var futureCounts = new List<QueryDeferred<int>>();
      var futureItems = new List<QueryFutureEnumerable<AggregateEntity>>();

      foreach (var modelTypeMeters in modelTypes)
      {
        var modelType = modelTypeMeters.Key;
        var meterIds = modelTypeMeters
          .Select(meter => meter.Id)
          .ToList();
        var entityType = modelEntityConverter.EntityType(modelType);

        var queryable = context
          .GetQueryable(entityType)
          as IQueryable<AggregateEntity>
          ?? throw new InvalidOperationException(
            $"No DbSet found for {entityType}");

        var parameter = Expression.Parameter(typeof(AggregateEntity), "entity");
        var foreignKeyExpression = Expression
          .Lambda<Func<AggregateEntity, bool>>(
            Expression.Invoke(
              context.ForeignKeyIn(
                entityType,
                nameof(AggregateEntity<MeterEntity>.Meter),
                meterIds),
              Expression.Convert(parameter, typeof(object))),
            parameter);

        var filtered = queryable
          .Where(aggregate => aggregate.Timestamp >= fromDate)
          .Where(aggregate => aggregate.Timestamp < toDate)
          .Where(aggregate => aggregate.Interval == appropriateInterval)
          .Where(foreignKeyExpression);

        var ordered = filtered
          .OrderBy(aggregate => aggregate.Timestamp);

        var paged = ordered
          .Skip((pageNumber - 1) * pageCount)
          .Take(pageCount);

        futureCounts.Add(filtered.DeferredCount());
        futureItems.Add(paged.Future());
      }

      foreach (var futureCount in futureCounts)
      {
        count += await futureCount.FutureValue().ValueAsync();
      }

      foreach (var futureItem in futureItems)
      {
        items.AddRange((await futureItem.ToListAsync())
          .Select(modelEntityConverter.ToModel)
          .OfType<IMeasurement>());
      }
    }
    else
    {
      var futureCounts = new List<QueryDeferred<int>>();
      var futureItems = new List<QueryFutureEnumerable<MeasurementEntity>>();

      foreach (var entityTypeMeters in modelTypes)
      {
        var modelType = entityTypeMeters.Key;
        var meterIds = entityTypeMeters
          .Select(meter => meter.Id)
          .ToList();
        var entityType = modelEntityConverter.EntityType(modelType);

        var queryable = context.GetQueryable(entityType)
          as IQueryable<MeasurementEntity>
          ?? throw new InvalidOperationException(
            $"No DbSet found for {entityType}");

        var parameter = Expression.Parameter(typeof(MeasurementEntity), "entity");
        var foreignKeyExpression = Expression
          .Lambda<Func<MeasurementEntity, bool>>(
            Expression.Invoke(
              context.ForeignKeyIn(
                entityType,
                nameof(MeasurementEntity<MeterEntity>.Meter),
                meterIds),
              Expression.Convert(parameter, typeof(object))),
            parameter);

        var filtered = queryable
          .Where(measurement => measurement.Timestamp >= fromDate)
          .Where(measurement => measurement.Timestamp < toDate)
          .Where(foreignKeyExpression);

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
        count += await futureCount.FutureValue().ValueAsync();
      }

      foreach (var futureItem in futureItems)
      {
        items.AddRange((await futureItem.ToListAsync())
          .Select(modelEntityConverter.ToModel)
          .OfType<IMeasurement>());
      }
    }

    return new PaginatedList<IMeasurement>(
      items,
      count
    );
  }
}
