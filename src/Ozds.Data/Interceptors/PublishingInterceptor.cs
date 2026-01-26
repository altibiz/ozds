using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Interceptors;

public class PublishingInterceptor(
  IServiceProvider serviceProvider)
  : EntityStateInterceptor(serviceProvider)
{
  public override int Order
  {
    get { return 0; }
  }

  public override InterceptionResult<int> SavingChanges(
    DbContextEventData eventData,
    InterceptionResult<int> result)
  {
    var baseResult = base.SavingChanges(eventData, result);

    var context = eventData.Context;
    if (context is null)
    {
      return baseResult;
    }

    var state = State(context);
    PublishEntitiesChanging(state.Entries);

    return baseResult;
  }

  public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
  {
    var baseResult = await base.SavingChangesAsync(
      eventData, result, cancellationToken);

    var context = eventData.Context;
    if (context is null)
    {
      return baseResult;
    }

    var state = AsyncState(context);
    PublishEntitiesChanging(state.Entries);

    return baseResult;
  }

  public override int SavedChanges(
    SaveChangesCompletedEventData eventData,
    int result)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return base.SavedChanges(eventData, result);
    }

    var state = State(context);
    PublishEntitiesChanged(state.Entries);

    return base.SavedChanges(eventData, result);
  }

  public override async ValueTask<int> SavedChangesAsync(
    SaveChangesCompletedEventData eventData,
    int result,
    CancellationToken cancellationToken = default
  )
  {
    var context = eventData.Context;
    if (context is null)
    {
      return await base.SavedChangesAsync(
        eventData, result, cancellationToken);
    }

    var state = AsyncState(context);
    PublishEntitiesChanged(state.Entries);

    return await base.SavedChangesAsync(
      eventData, result, cancellationToken);
  }

  private void PublishEntitiesChanging(List<EntityStateEntry> entries)
  {
    var publisher = serviceProvider
      .GetRequiredService<IEntitiesChangingPublisher>();

    publisher.Publish(
      new EntitiesChangingEventArgs
      {
        Entities = entries
          .Select(entry => new EntityChangingEntry(
              entry.State switch
              {
                EntityState.Added => EntityChangingState.Adding,
                EntityState.Modified => EntityChangingState.Modifying,
                EntityState.Deleted => EntityChangingState.Removing,
                _ => throw new NotImplementedException()
              },
              entry.Entity
            )
          )
          .ToList()
      });
  }

  private void PublishEntitiesChanged(List<EntityStateEntry> entries)
  {
    var publisher = serviceProvider
      .GetRequiredService<IEntitiesChangedPublisher>();

    publisher.Publish(
      new EntitiesChangedEventArgs
      {
        Entities = entries
          .Select(entry => new EntityChangedEntry(
              entry.State switch
              {
                EntityState.Added => EntityChangedState.Added,
                EntityState.Modified => EntityChangedState.Modified,
                EntityState.Deleted => EntityChangedState.Removed,
                _ => throw new NotImplementedException()
              },
              entry.Entity
            )
          )
          .ToList()
      });
  }
}
