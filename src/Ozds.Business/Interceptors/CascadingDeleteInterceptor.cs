using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Business.Extensions;
using Ozds.Data;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Business.Interceptors;

public class CascadingDeleteInterceptor : ServedSaveChangesInterceptor
{
  public CascadingDeleteInterceptor(IServiceProvider serviceProvider)
    : base(serviceProvider)
  {
  }

  public override int Order
  {
    get { return 10; }
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    await AddCascadingDeletes(eventData);
    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static async ValueTask AddCascadingDeletes(
    DbContextEventData eventData)
  {
    var context = eventData.Context as OzdsDataDbContext;
    if (context is null)
    {
      return;
    }

    var entries = context.ChangeTracker.Entries<IIdentifiableEntity>().ToList();

    foreach (var entry in entries.Where(e => e.State is EntityState.Deleted))
    {
      await CascadingDelete(
        eventData,
        entry
      );
    }
  }

  private static async Task CascadingDelete(
    DbContextEventData eventData,
    EntityEntry entry)
  {
    var context = eventData.Context as OzdsDataDbContext;
    if (context is null)
    {
      return;
    }

    var entity = entry.Entity as IIdentifiableEntity;
    if (entity is null)
    {
      return;
    }

    var allRelationships = context.Model
      .GetEntityTypes()
      .SelectMany(e => e.GetForeignKeys())
      .Where(relationship => relationship.IsRequired);

    var relationships = allRelationships
      .Where(
        relationship => relationship.PrincipalEntityType
          == entry.Metadata);

    foreach (var relationship in relationships
      .Where(
        relationship => !relationship.DeclaringEntityType.ClrType
          .IsAssignableTo(typeof(IReadonlyEntity)))
      .Where(
        relationship => relationship.DeclaringEntityType.ClrType
          .IsAssignableTo(typeof(IIdentifiableEntity))))
    {
      var declarers = await context
        .GetQueryable(relationship.DeclaringEntityType.ClrType)
        .Where(
          context.ForeignKeyEqualsAgnostic(
            relationship.DeclaringEntityType.ClrType,
            relationship.GetNavigation(true)?.Name
            ?? throw new InvalidOperationException(
              "No navigation property found"),
            entity.Id))
        .ToListAsync();

      foreach (var declaring in declarers)
      {
        var declaringEntry = context.FindEntry(declaring);
        declaringEntry.State = EntityState.Deleted;

        await CascadingDelete(
          eventData,
          declaringEntry
        );
      }
    }
  }
}
