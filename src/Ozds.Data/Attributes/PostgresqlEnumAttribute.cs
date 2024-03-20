using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Enum)]
public class PostgresqlEnumAttribute : Attribute
{
}

public static class PostgresqlEnumAttributeExtensions
{
  public static ModelBuilder ApplyPostgresqlEnums(this ModelBuilder builder) =>
    builder.Model
      .GetEntityTypes()
      .SelectMany(entityType => entityType.GetProperties())
      .Where(
        property =>
          property.PropertyInfo?.PropertyType is { } type &&
          type.GetCustomAttribute<PostgresqlEnumAttribute>() is { }
      )
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

  private static readonly MethodInfo HasPostgresEnumMethod =
    typeof(NpgsqlModelBuilderExtensions)
      .GetMethods()
      .First(
        method =>
          method.IsGenericMethod
          && method.Name == nameof(NpgsqlModelBuilderExtensions.HasPostgresEnum)
      );
}

