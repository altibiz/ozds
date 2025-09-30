using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ozds.Data.Entities.Abstractions;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Data.Interceptors;

public sealed record EntityProperty(
  string Name,
  string? OldValue,
  string? NewValue);

public record EntityChangesEntry(
  EntityState State,
  EntityProperty[] Properties,
  IEntity Entity,
  EntityEntry Original
);

public record EntityChangesInterceptorState(
  List<EntityChangesEntry> Entries,
  DateTimeOffset Now
);

public class EntityChangesInterceptor(IServiceProvider serviceProvider)
  : StatefulInterceptor<EntityChangesInterceptorState>(serviceProvider)
{
  protected override EntityChangesInterceptorState ProcessSavingChanges(
    DbContext context)
  {
    var clockQueries = serviceProvider.GetRequiredService<IClockQueries>();

    var timestamp = clockQueries.Timestamp();

    var entries = new List<EntityChangesEntry>();

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

      var properties = entry.Properties
        .Where(
          property =>
            property.OriginalValue?.ToString()
            != property.CurrentValue?.ToString())
        .Select(
          property => new EntityProperty(
            property.Metadata.Name,
            property.OriginalValue?.ToString(),
            property.CurrentValue?.ToString()
          ))
        .ToArray();

      entries.Add(
        new EntityChangesEntry(
          entry.State switch
          {
            Microsoft.EntityFrameworkCore.EntityState.Added =>
              EntityState.Added,
            Microsoft.EntityFrameworkCore.EntityState.Modified =>
              EntityState.Modified,
            Microsoft.EntityFrameworkCore.EntityState.Deleted =>
              EntityState.Deleted,
            _ => throw new NotImplementedException()
          },
          properties,
          entity,
          entry));
    }

    return new EntityChangesInterceptorState(entries, timestamp);
  }
}
