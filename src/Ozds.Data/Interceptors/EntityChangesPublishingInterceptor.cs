using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Interceptors;

public class EntityChangesPublishingInterceptor(
  IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  private readonly ConditionalWeakTable<DbContext, List<EntityChangesEntry>>
    _contextEntries = new();

  public override int Order
  {
    get { return 0; }
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return base.SavingChanges(eventData, result);
    }

    var entries = ProcessSavingChanges(context);
    _contextEntries.Add(context, entries);
    PublishEntitiesChanging(entries);

    return base.SavingChanges(eventData, result);
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return await base.SavingChangesAsync(
        eventData, result, cancellationToken);
    }

    var entries = ProcessSavingChanges(context);
    _contextEntries.Add(context, entries);
    PublishEntitiesChanging(entries);

    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public override int SavedChanges(
    SaveChangesCompletedEventData eventData,
    int result)
  {
    var context = eventData.Context;
    if (context == null)
    {
      return base.SavedChanges(eventData, result);
    }

    if (_contextEntries.TryGetValue(context, out var entries))
    {
      PublishEntitiesChanged(entries);
      _contextEntries.Remove(context);
    }
    return base.SavedChanges(eventData, result);
  }

  public override async ValueTask<int> SavedChangesAsync(
    SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default
  )
  {
    var context = eventData.Context;
    if (context == null)
    {
      return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    if (_contextEntries.TryGetValue(context, out var entries))
    {
      PublishEntitiesChanged(entries);
      _contextEntries.Remove(context);
    }
    return await base.SavedChangesAsync(eventData, result, cancellationToken);
  }

  private List<EntityChangesEntry> ProcessSavingChanges(DbContext context)
  {
    context.ChangeTracker.DetectChanges();

    var entries = new List<EntityChangesEntry>();

    foreach (var entry in context.ChangeTracker.Entries())
    {
      if (entry.Entity is not IEntity entity)
      {
        continue;
      }

      if (entry.State is not EntityState.Added
        and not EntityState.Modified
        and not EntityState.Deleted)
      {
        continue;
      }

      entries.Add(
        new EntityChangesEntry(
          entry.State switch
          {
            EntityState.Added => EntityChanges.Added,
            EntityState.Modified => EntityChanges.Modified,
            EntityState.Deleted => EntityChanges.Deleted,
            _ => throw new NotImplementedException()
          },
          entity));
    }
    return entries;
  }

  private void PublishEntitiesChanging(List<EntityChangesEntry> entries)
  {
    var publisher = serviceProvider
      .GetRequiredService<IEntityChangesPublisher>();

    publisher.PublishEntitiesChanging(
      new EntitiesChangingEventArgs
      {
        Entities = entries
          .Select(
            entry => new EntityChangingRecord(
              entry.State switch
              {
                EntityChanges.Added => EntityChangingState.Adding,
                EntityChanges.Modified => EntityChangingState.Modifying,
                EntityChanges.Deleted => EntityChangingState.Removing,
                _ => throw new NotImplementedException()
              },
              entry.Entity
            )
          )
          .ToList()
      });
  }

  private void PublishEntitiesChanged(List<EntityChangesEntry> entries)
  {
    var publisher = serviceProvider
      .GetRequiredService<IEntityChangesPublisher>();

    publisher.PublishEntitiesChanged(
      new EntitiesChangedEventArgs
      {
        Entities = entries
          .Select(
            entry => new EntityChangedEntry(
              entry.State switch
              {
                EntityChanges.Added => EntityChangedState.Added,
                EntityChanges.Modified => EntityChangedState.Modified,
                EntityChanges.Deleted => EntityChangedState.Removed,
                _ => throw new NotImplementedException()
              },
              entry.Entity
            )
          )
          .ToList()
      });
  }

  private enum EntityChanges
  {
    Added,
    Modified,
    Deleted
  }

  private sealed record EntityChangesEntry(
    EntityChanges State,
    IEntity Entity
  );
}
