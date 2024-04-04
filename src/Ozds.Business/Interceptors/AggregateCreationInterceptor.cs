using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
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
        .IsAssignableTo(typeof(MeasurementEntity)));

    foreach (var entityType in measurementEntityTypes)
    {
      var entities = context.ChangeTracker
        .Entries()
        .Where(entity => entity.Entity.GetType() == entityType.ClrType)
        .Select(entity => entity.Entity)
        .OfType<MeasurementEntity>();

      if (entities is null)
      {
        continue;
      }

      var measurementAggregateConverters = _serviceProvider
        .GetServices<IMeasurementAggregateConverter>();
      var modelEntityConverters = _serviceProvider
        .GetServices<IModelEntityConverter>();
      var aggregateUpserters = _serviceProvider
        .GetServices<IAggregateUpserter>();

      var aggregates = context.ChangeTracker
        .Entries()
        .Where(entity => entity.State == EntityState.Added)
        .Select(entity => entity.Entity)
        .OfType<MeasurementEntity>()
        .Select(entity => modelEntityConverters
          .FirstOrDefault(converter => converter
            .CanConvertToEntity(entity.GetType()))
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
        var aggregateUpserter = _serviceProvider
          .GetServices<IAggregateUpserter>()
          .FirstOrDefault(upserter => upserter
            .CanUpsertEntity(group.Key));
        if (aggregateUpserter is null)
        {
          continue;
        }

        await context
          .UpsertRange(group.ToArray())
          .On(entity => new
          {
            entity.MeterId,
            entity.Timestamp,
            entity.Interval
          })
          .WhenMatched(aggregateUpserter.UpsertEntity)
          .RunAsync();
      }
    }
  }
}
