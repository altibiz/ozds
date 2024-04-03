using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Interceptors;

public class AggregateCreationInterceptor : ServedSaveChangesInterceptor
{
  public AggregateCreationInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData, InterceptionResult<int> result)
  {
    CreateAggregates(eventData).RunSynchronously();
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData, InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    await CreateAggregates(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static async Task CreateAggregates(DbContextEventData eventData)
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

    async Task CreateAggregates<T>(IEnumerable<T> aggregates) where T : IUpsertAggregate<T>
    {
      await context
        .UpsertRange(
          aggregates
            .Select(EntityModelTypeMapper.ToEntity)
        )
        .On(entity => new
        {
          entity.MeterId,
          entity.Timestamp,
          entity.Interval
        })
        .WhenMatched()
        .RunAsync();
    }

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

      await context
        .UpsertRange(
          context.ChangeTracker
            .Entries()
            .Where(entity => entity.Entity is MeasurementEntity)
            .Where(entity => entity.State == EntityState.Added)
            .Select(entity => entity.Entity)
            .OfType<MeasurementEntity>()
            .Select(EntityModelTypeMapper.ToModel)
            .Select(EntityModelTypeMapper.ToAggregate)
            .OfType<IAggregate>()
            .UpsertRange()
            .Select(EntityModelTypeMapper.ToEntity)
            .OfType<AggregateEntity>())
        .On(entity => new
        {
          entity.MeterId,
          entity.Timestamp,
          entity.Interval
        })
        .RunAsync();
    }
  }
}
