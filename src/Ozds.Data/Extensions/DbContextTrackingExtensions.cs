using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ozds.Data.Extensions;

public static class DbContextTrackingExtensions
{
  public static void AddTracked(
    this DbContext context,
    object entity
  )
  {
    var entry = context.FindEntry(entity);
    entry.CurrentValues.SetValues(entity);
    entry.State = EntityState.Added;
  }

  public static void UpdateTracked(
    this DbContext context,
    object entity
  )
  {
    var entry = context.FindEntry(entity);
    entry.CurrentValues.SetValues(entity);
    entry.State = EntityState.Modified;
  }

  public static void RemoveTracked(
    this DbContext context,
    object entity
  )
  {
    var entry = context.FindEntry(entity);
    entry.State = EntityState.Deleted;
  }

  public static void JoinTracked<T>(
    this DbContext context,
    object entity,
    ICollection<T> collection)
    where T : notnull
  {
    var entry = context.FindEntry(entity);

    if (entry.Collections
        .FirstOrDefault(
          collection =>
            collection.Metadata.TargetEntityType.ClrType == typeof(T))
      is not { } entryCollection)
    {
      throw new InvalidOperationException(
        $"No collection of {typeof(T)} found on {entity.GetType()}");
    }

    entryCollection.CurrentValue = collection
      .Select(entity => context.FindEntry(entity))
      .Select(entry => entry.Entity)
      .OfType<T>()
      .ToList();
  }

  public static EntityEntry FindEntry(
    this DbContext context,
    object entity
  )
  {
    var entry = context.ChangeTracker
      .Entries()
      .FirstOrDefault(entry => entry.HasSamePrimaryKey(entity));
    if (entry is null)
    {
      return context.Entry(entity);
    }

    return entry;
  }

  private static bool HasSamePrimaryKey(
    this EntityEntry entry,
    object entity
  )
  {
    var entityTypeAssembly = entry.Entity.GetType().Assembly.FullName
      ?? throw new InvalidOperationException(
        "Entity type has no assembly.");
    var entityType = entityTypeAssembly.StartsWith("DynamicProxyGenAssembly2")
      ? entry.Entity.GetType().BaseType ??
      throw new InvalidOperationException("Proxy has no base type.")
      : entry.Entity.GetType();
    return entityType.IsInstanceOfType(entity) &&
      (entry.Metadata
        .FindPrimaryKey()?.Properties
        .All(
          property => property
            .GetGetter()
            .GetClrValue(entity)?.Equals(
              property
                .GetGetter()
                .GetClrValue(entry.Entity)) ?? false) ?? false);
  }
}
