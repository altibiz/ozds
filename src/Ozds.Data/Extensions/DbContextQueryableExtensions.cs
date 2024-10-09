using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Extensions;

public static class DbContextQueryableExtensions
{
  public static IQueryable<T> GetQueryable<T>(
    this DbContext context,
    Type entityType
  )
  {
    return context.GetQueryable(entityType) as IQueryable<T>
      ?? throw new InvalidOperationException($"No DbSet found for {typeof(T)}");
  }

  public static IQueryable<object> GetQueryable(
    this DbContext context,
    Type type)
  {
    var method = typeof(DbContext)
      .GetMethods()
      .FirstOrDefault(
        m => m.Name == nameof(DbContext.Set)
          && m.IsGenericMethodDefinition
          && m.GetParameters().Length == 0)
      ?.MakeGenericMethod(type);
    return method?.Invoke(context, null) as IQueryable<object>
      ?? throw new InvalidOperationException($"No DbSet found for {type}");
  }
}
