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
  private readonly List<EntityChangesEntry> _entries = new();

  private readonly SemaphoreSlim _semaphore = new(1, 1);

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

    _semaphore.Wait();
    ProcessSavingChanges(context);
    PublishEntitiesChanging();

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

    await _semaphore.WaitAsync(cancellationToken);
    ProcessSavingChanges(context);
    PublishEntitiesChanging();

    return await base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  public override int SavedChanges(
    SaveChangesCompletedEventData eventData,
    int result)
  {
    PublishEntitiesChanged();
    ProcessSavedChanges();
    _semaphore.Release();
    return base.SavedChanges(eventData, result);
  }

  public override ValueTask<int> SavedChangesAsync(
    SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default
  )
  {
    PublishEntitiesChanged();
    ProcessSavedChanges();
    _semaphore.Release();
    return base.SavedChangesAsync(eventData, result, cancellationToken);
  }

  private void ProcessSavingChanges(DbContext context)
  {
    context.ChangeTracker.DetectChanges();

    foreach (var entry in context.ChangeTracker.Entries())
    {
      if (entry.Entity is not IEntity entity)
      {
        continue;
      }

      if (entry.State is not EntityState.Added
        or EntityState.Modified
        or EntityState.Deleted)
      {
        continue;
      }

      _entries.Add(
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
  }

  private void ProcessSavedChanges()
  {
    _entries.Clear();
  }

  private void PublishEntitiesChanging()
  {
    var publisher = serviceProvider
      .GetRequiredService<IEntityChangesPublisher>();

    publisher.PublishEntitiesChanging(
      new EntitiesChangingEventArgs
      {
        Entities = _entries
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

  private void PublishEntitiesChanged()
  {
    var publisher = serviceProvider
      .GetRequiredService<IEntityChangesPublisher>();

    publisher.PublishEntitiesChanged(
      new EntitiesChangedEventArgs
      {
        Entities = _entries
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
