using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ozds.Data.Entities.Abstractions;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Data.Interceptors;

public enum EntityState
{
  Added,
  Modified,
  Deleted
}

public record EntityStateEntry(
  EntityState State,
  IEntity Entity,
  EntityEntry Original
);

public record EntityStateInterceptorState(
  List<EntityStateEntry> Entries,
  DateTimeOffset Now
);

public abstract class EntityStateInterceptor(IServiceProvider serviceProvider)
  : StatefulInterceptor<EntityStateInterceptorState>(serviceProvider)
{
  protected override EntityStateInterceptorState ProcessSavingChanges(
    DbContext context)
  {
    var clockQueries = serviceProvider.GetRequiredService<IClockQueries>();

    var timestamp = clockQueries.Timestamp();

    var entries = new List<EntityStateEntry>();

    foreach (var entry in context.ChangeTracker.Entries())
    {
      if (entry.Entity is not IEntity entity)
      {
        continue;
      }

      if (entry.State is not Microsoft.EntityFrameworkCore.EntityState.Added
        and not Microsoft.EntityFrameworkCore.EntityState.Modified
        and not Microsoft.EntityFrameworkCore.EntityState.Deleted)
      {
        continue;
      }

      entries.Add(
        new EntityStateEntry(
          entry.State switch
          {
            Microsoft.EntityFrameworkCore.EntityState.Added =>
              EntityState.Added,
            Microsoft.EntityFrameworkCore.EntityState.Modified => EntityState
              .Modified,
            Microsoft.EntityFrameworkCore.EntityState.Deleted => EntityState
              .Deleted,
            _ => throw new NotImplementedException()
          },
          entity,
          entry));
    }

    return new EntityStateInterceptorState(entries, timestamp);
  }
}
