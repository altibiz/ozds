using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Observers.Abstractions;

namespace Ozds.Data.Interceptors;

public class PublishingInterceptor(IServiceProvider serviceProvider)
  : ServedSaveChangesInterceptor(serviceProvider)
{
  public override int SavedChanges(
    SaveChangesCompletedEventData eventData,
    int result)
  {
    var context = eventData.Context;
    if (context is null)
    {
      return base.SavedChanges(eventData, result);
    }

    var publisher = serviceProvider
      .GetRequiredService<IEntityChangesPublisher>();

    foreach (var entry in context.ChangeTracker.Entries())
    {
      if (entry.Entity is not IEntity entity)
      {
        continue;
      }

      if (entry.State == EntityState.Added)
      {
        publisher.PublishEntityAdded(entity);
      }

      if (entry.State == EntityState.Modified)
      {
        publisher.PublishEntityModified(entity);
      }

      if (entry.State == EntityState.Deleted)
      {
        publisher.PublishEntityRemoved(entity);
      }
    }

    return base.SavedChanges(eventData, result);
  }
}
