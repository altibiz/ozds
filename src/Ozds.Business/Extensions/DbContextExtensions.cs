using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

// TODO: add tracked - create a way to make distinct ephemeral primary keys

namespace Ozds.Business.Extensions;

public static class DbContextExtensions
{
  public static DbSet<object>? GetDbSet(this DbContext context, Type type)
  {
    var method = typeof(DbContext)
      .GetMethods()
      .FirstOrDefault(m => m.Name == nameof(DbContext.Set)
                           && m.IsGenericMethodDefinition
                           && m.GetParameters().Length == 0)
      ?.MakeGenericMethod(type);
    return method?.Invoke(context, null) as DbSet<object>;
  }

  public static void AddTracked(
    this DbContext context,
    object entity
  )
  {
    context.AddOrUpdateEntry(
      entry => entry.HasSamePrimaryKey(entity),
      () => entity,
      entry =>
      {
        entry.CurrentValues.SetValues(entity);
        entry.State = EntityState.Added;
      }
    );
  }

  public static void UpdateTracked(
    this DbContext context,
    object entity
  )
  {
    context.AddOrUpdateEntry(
      entry => entry.HasSamePrimaryKey(entity),
      () => entity,
      entry =>
      {
        entry.CurrentValues.SetValues(entity);
        entry.State = EntityState.Modified;
      }
    );
  }

  public static void DeleteTracked(
    this DbContext context,
    object entity
  )
  {
    context.AddOrUpdateEntry(
      entry => entry.HasSamePrimaryKey(entity),
      () => entity,
      entry =>
      {
        entry.CurrentValues.SetValues(entity);
        entry.State = EntityState.Added;
      }
    );
  }

  private static bool HasSamePrimaryKey(
    this EntityEntry entry,
    object entity
  )
  {
    return entry.Metadata
      .FindPrimaryKey()?.Properties
      .All(property => property
        .GetGetter()
        .GetClrValue(entity)?.Equals(property
          .GetGetter()
          .GetClrValue(entry.Entity)) ?? false) ?? false;
  }

  private static void AddOrUpdateEntry(
    this DbContext context,
    Func<EntityEntry, bool> predicate,
    Func<object> factory,
    Action<EntityEntry> action
  )
  {
    var entry = context.ChangeTracker.Entries().FirstOrDefault(predicate)
                ?? context.Entry(factory());
    action(entry);
  }

  private static void AddOrUpdateEntries(
    this DbContext context,
    Func<EntityEntry, bool> predicate,
    Action<EntityEntry> action
  )
  {
    foreach (var entry in context.ChangeTracker.Entries().Where(predicate))
    {
      action(entry);
    }
  }
}
