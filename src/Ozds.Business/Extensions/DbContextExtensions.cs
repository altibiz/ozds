using Microsoft.EntityFrameworkCore;

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
}
