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
    CreateAggregates(eventData);
    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData, InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    CreateAggregates(eventData);
    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static void CreateAggregates(DbContextEventData eventData)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    var aggregates = context.ChangeTracker.Entries<MeasurementEntity>()
      .Where(entity => entity.State == EntityState.Added)
      .Select(entity => entity.Entity)
      .Select(EntityModelTypeMapper.ToModel)
      .Select(EntityModelTypeMapper.ToAggregate)
      .OfType<IAggregate>()
      .UpsertRange()
      .Select(EntityModelTypeMapper.ToEntity)
      .ToList();
    foreach (var aggregate in aggregates)
    {
      context.Add(aggregate);
    }
  }
}
