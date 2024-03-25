using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class PostgresqlEnumAttribute : Attribute
{
}

public static class PostgresqlEnumAttributeExtensions
{
  private static readonly MethodInfo HasPostgresEnumMethod =
    typeof(NpgsqlModelBuilderExtensions)
      .GetMethods()
      .First(
        method =>
          method.IsGenericMethod
          && method.Name == nameof(NpgsqlModelBuilderExtensions.HasPostgresEnum)
      );

  public static ModelBuilder ApplyPostgresqlEnums(this ModelBuilder builder)
  {
    return builder.Model
      .GetEntityTypes()
      .SelectMany(entityType => entityType.GetProperties())
      .Where(
        property =>
          property.PropertyInfo?.PropertyType is { } type &&
          type.GetCustomAttribute<PostgresqlEnumAttribute>() is not null)
      .Aggregate(
        builder,
        (builder, property) =>
        {
          HasPostgresEnumMethod
            .MakeGenericMethod(property.PropertyInfo!.PropertyType)
            .Invoke(
              null,
              new[] { builder, null, null, null }
            );

          return builder;
        }
      );
  }
}
