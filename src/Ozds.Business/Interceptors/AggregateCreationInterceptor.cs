using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Business.Conversion.Abstractions;
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

    var measurementAggregateConverters = aggregateScope.ServiceProvider
      .GetServices<IMeasurementAggregateConverter>();
    var modelEntityConverters = aggregateScope.ServiceProvider
      .GetServices<IModelEntityConverter>();
    var aggregateUpserters = aggregateScope.ServiceProvider
      .GetServices<IAggregateUpserter>();
    var aggregateContext = aggregateScope.ServiceProvider
      .GetRequiredService<OzdsDbContext>();

    var aggregates = context.ChangeTracker
      .Entries()
      .Where(entity => entity.State == EntityState.Added)
      .Select(entity => entity.Entity)
      .OfType<MeasurementEntity>()
      .Select(entity => modelEntityConverters
        .FirstOrDefault(converter => converter
          .CanConvertToModel(entity.GetType()))
        ?.ToModel(entity))
      .OfType<IMeasurement>()
      .SelectMany(model => typeof(IntervalModel)
        .GetEnumValues()
        .Cast<IntervalModel>()
        .Select(interval =>
          measurementAggregateConverters
            .FirstOrDefault(converter => converter
              .CanConvertToAggregate(model.GetType()))
            ?.ToAggregate(model, interval)))
      .OfType<IAggregate>()
      .GroupBy(aggregate => new
      {
        Type = aggregate.GetType(),
        aggregate.MeterId,
        aggregate.Timestamp,
        aggregate.Interval
      })
      .Select(group => group
        .Aggregate((lhs, rhs) => lhs is not null && rhs is not null
          ? aggregateUpserters
            .FirstOrDefault(upserter => upserter
              .CanUpsertModel(group.Key.Type))
            ?.UpsertModel(lhs, rhs)!
          : default!))
      .OfType<IAggregate>()
      .Select(model => modelEntityConverters
        .FirstOrDefault(converter => converter
          .CanConvertToEntity(model.GetType()))
        ?.ToEntity(model))
      .OfType<IAggregateEntity>()
      .GroupBy(entity => entity.GetType());

    foreach (var group in aggregates)
    {
      var aggregateUpserter = aggregateUpserters
        .FirstOrDefault(upserter => upserter
          .CanUpsertEntity(group.Key));
      if (aggregateUpserter is null)
      {
        continue;
      }

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
    IAggregateUpserter upserter)
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
      .WhenMatched(
        upserter.UpsertEntity as Expression<Func<T, T, T>>
        ?? throw new InvalidOperationException(
            $"Upsert entity is not of type {typeof(T).Name}."))
      .RunAsync();
  }
}
