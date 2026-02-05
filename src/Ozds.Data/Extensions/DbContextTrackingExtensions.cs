using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ozds.Data.Extensions;

public static class DbContextTrackingExtensions
{
  public static EntityEntry<T> FindEntry<T>(this DbContext context, T entity)
    where T : class
  {
    var entry = context
      .ChangeTracker.Entries<T>()
      .FirstOrDefault(entry => entry.HasSamePrimaryKey(entity));
    if (entry is null)
    {
      return context.Entry(entity);
    }

    return entry;
  }

  private static bool HasSamePrimaryKey(this EntityEntry entry, object entity)
  {
    var entityTypeAssembly =
      entry.Entity.GetType().Assembly.FullName
      ?? throw new InvalidOperationException("Entity type has no assembly.");
    var entityType = entityTypeAssembly.StartsWith("DynamicProxyGenAssembly2")
      ? entry.Entity.GetType().BaseType
        ?? throw new InvalidOperationException("Proxy has no base type.")
      : entry.Entity.GetType();
    return entityType.IsInstanceOfType(entity)
      && (
        entry
          .Metadata.FindPrimaryKey()
          ?.Properties.All(property =>
            property
              .GetGetter()
              .GetClrValue(entity)
              ?.Equals(property.GetGetter().GetClrValue(entry.Entity))
            ?? false
          )
        ?? false
      );
  }
}
