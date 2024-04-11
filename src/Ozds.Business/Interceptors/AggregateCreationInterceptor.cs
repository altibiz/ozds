using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Aggregation.Agnostic;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Data;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Interceptors;

public class AggregateCreationInterceptor : ServedSaveChangesInterceptor
{
  public AggregateCreationInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData, InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    await CreateAggregates(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private async Task CreateAggregates(DbContextEventData eventData)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    var measurementEntityTypes = context.Model
      .GetEntityTypes()
      .Where(entityType => entityType.ClrType
        .IsAssignableTo(typeof(MeasurementEntity)))
      .ToArray();

    if (measurementEntityTypes is null || measurementEntityTypes.Length == 0)
    {
      return;
    }

    await using var aggregateScope = _serviceProvider.CreateAsyncScope();

    var aggregateConverter = aggregateScope.ServiceProvider
      .GetRequiredService<AgnosticMeasurementAggregateConverter>();
    var modelEntityConverter = aggregateScope.ServiceProvider
      .GetRequiredService<AgnosticModelEntityConverter>();
    var aggregateUpserter = aggregateScope.ServiceProvider
      .GetRequiredService<AgnosticAggregateUpserter>();
    var aggregateContext = aggregateScope.ServiceProvider
      .GetRequiredService<OzdsDbContext>();

    var aggregates = context.ChangeTracker
      .Entries()
      .Where(entity => entity.State == EntityState.Added)
      .Select(entity => entity.Entity)
      .OfType<MeasurementEntity>()
      .Select(modelEntityConverter.ToModel)
      .OfType<IMeasurement>()
      .SelectMany(model => typeof(IntervalModel)
        .GetEnumValues()
        .Cast<IntervalModel>()
        .Select(interval => aggregateConverter.ToAggregate(model, interval)))
      .OfType<IAggregate>()
      .GroupBy(aggregate => new
      {
        Type = aggregate.GetType(),
        aggregate.MeterId,
        aggregate.Timestamp,
        aggregate.Interval
      })
      .Select(group => group.Aggregate(aggregateUpserter.UpsertModelAgnostic))
      .Select(modelEntityConverter.ToEntity)
      .OfType<IAggregateEntity>()
      .GroupBy(entity => entity.GetType());

    foreach (var group in aggregates)
    {
      var enumerableCastMethod = typeof(Enumerable)
        .GetMethod(nameof(Enumerable.Cast),
          BindingFlags.Public | BindingFlags.Static)
        ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(Enumerable.Cast)}.");
      var upsertAggregatesMethod = typeof(AggregateCreationInterceptor)
        .GetMethod(nameof(UpsertAggregates),
          BindingFlags.NonPublic | BindingFlags.Static)
        ?.MakeGenericMethod(group.Key)
        ?? throw new InvalidOperationException(
          $"Cannot find method {nameof(UpsertAggregates)}.");
      var result = upsertAggregatesMethod.Invoke(null, new object[]
      {
          aggregateContext,
          enumerableCastMethod.Invoke(null, new object[] { group })
            ?? throw new InvalidOperationException(
              $"Cannot cast group to {group.Key.Name}."),
          aggregateUpserter
      });

      if (result is Task task)
      {
        await task;
      }
    }
  }

  private static async Task UpsertAggregates<T>(
    DbContext context,
    IEnumerable<T> aggregates,
    AgnosticAggregateUpserter upserter)
    where T : class, IAggregateEntity
  {
    await context
      .UpsertRange(aggregates.ToArray())
      .On(aggregate => new
      {
        aggregate.MeterId,
        aggregate.Timestamp,
        aggregate.Interval
      })
      .WhenMatched(upserter.UpsertEntity<T>())
      .RunAsync();
  }
}
