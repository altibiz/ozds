using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Interceptors;

public class CascadingRestoreInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override int Order
  {
    get { return 11; }
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result
  )
  {
    AddCascadingRestores(eventData).GetAwaiter().GetResult();
    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    await AddCascadingRestores(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static async Task AddCascadingRestores(
    DbContextEventData eventData)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    context.ChangeTracker.DetectChanges();
    var entries = context.ChangeTracker.Entries<IAuditableEntity>().ToList();

    foreach (var entry in entries.Where(
      e =>
        e.State is EntityState.Added
        && e.Entity.IsDeleted
        && e.Entity.Restore))
    {
      await CascadingRestore(
        eventData,
        entry
      );
    }
  }

  private static async Task CascadingRestore(
    DbContextEventData eventData,
    EntityEntry<IAuditableEntity> entry)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return;
    }

    var relationships = context.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetForeignKeys())
      .Where(relationship => relationship.IsRequired)
      .Where(
        relationship => relationship.PrincipalEntityType
          == entry.Metadata);

    foreach (var relationship in relationships
      .Where(
        relationship => relationship.DeclaringEntityType.ClrType
          .IsAssignableTo(typeof(IAuditableEntity))))
    {
      var declarers = await context
        .GetQueryable(relationship.DeclaringEntityType.ClrType)
        .Where(
          context.ForeignKeyEquals(
            relationship.DeclaringEntityType.ClrType,
            relationship.GetNavigation(true)?.Name
            ?? throw new InvalidOperationException(
              "No navigation property found"),
            entry.Entity.Id))
        .OfType<IAuditableEntity>()
        .ToListAsync();

      foreach (var declaring in declarers)
      {
        var declaringEntry = context.FindEntry(declaring);
        declaringEntry.State = EntityState.Added;
        declaringEntry.Entity.Restore = entry.Entity.Restore;
        await CascadingRestore(
          eventData,
          declaringEntry
        );
      }
    }
  }
}
