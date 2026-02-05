using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Interceptors;

public class CascadingDeleteInterceptor(IServiceProvider serviceProvider)
  : ServedInterceptor(serviceProvider)
{
  public override int Order
  {
    get { return 10; }
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    AddCascadingDeletes(eventData).GetAwaiter().GetResult();
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    await AddCascadingDeletes(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static async Task AddCascadingDeletes(DbContextEventData eventData)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    context.ChangeTracker.DetectChanges();
    var entries = context.ChangeTracker.Entries<ITrackableEntity>().ToList();

    foreach (
      var entry in entries.Where(e =>
        e.State is Microsoft.EntityFrameworkCore.EntityState.Deleted
      )
    )
    {
      await CascadingDelete(eventData, entry);
    }
  }

  private static async Task CascadingDelete(
    DbContextEventData eventData,
    EntityEntry<ITrackableEntity> entry
  )
  {
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    var relationships = context
      .Model.GetEntityTypes()
      .SelectMany(e => e.GetForeignKeys())
      .Where(relationship => relationship.IsRequired)
      .Where(relationship =>
        relationship.PrincipalEntityType == entry.Metadata
      );

    foreach (
      var relationship in relationships.Where(relationship =>
        relationship.DeclaringEntityType.ClrType.IsAssignableTo(
          typeof(ITrackableEntity)
        )
      )
    )
    {
      var declarers = await context
        .GetQueryable(relationship.DeclaringEntityType.ClrType)
        .Where(
          context.ForeignKeyEquals(
            relationship.DeclaringEntityType.ClrType,
            relationship.GetNavigation(true)?.Name
              ?? throw new InvalidOperationException(
                "No navigation property found"
              ),
            entry.Entity.AuditingId
          )
        )
        .OfType<ITrackableEntity>()
        .ToListAsync();

      foreach (var declaring in declarers)
      {
        var declaringEntry = context.FindEntry(declaring);
        declaringEntry.State = Microsoft
          .EntityFrameworkCore
          .EntityState
          .Deleted;
        declaringEntry.Entity.Forget = entry.Entity.Forget;

        await CascadingDelete(eventData, declaringEntry);
      }
    }
  }
}

public class CascadingDeleteInterceptorModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    foreach (
      var relationship in modelBuilder
        .Model.GetEntityTypes()
        .SelectMany(e => e.GetForeignKeys())
    )
    {
      if (relationship.IsRequired)
      {
        if (
          relationship.DeclaringEntityType.ClrType.IsAssignableTo(
            typeof(ITrackableEntity)
          )
        )
        {
          relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        else
        {
          relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
      }
      else
      {
        relationship.DeleteBehavior = DeleteBehavior.SetNull;
      }
    }
  }
}
