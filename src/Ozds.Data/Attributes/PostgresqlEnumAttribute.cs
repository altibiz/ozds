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

  public static ModelBuilder ApplyPostgresqlEnumAttributes(
    this ModelBuilder builder)
  {
    return builder.Model
      .GetEntityTypes()
      .SelectMany(
        entityType => entityType
          .GetProperties()
          .SelectMany(
            property =>
              property.PropertyInfo?.PropertyType is { } type
                ? RecursivelyFindPostgresqlEnumTypes(type)
                : Enumerable.Empty<Type>()))
      .Aggregate(
        builder,
        (builder, type) =>
        {
          HasPostgresEnumMethod
            .MakeGenericMethod(type)
            .Invoke(
              null,
              new[] { builder, null, null, null }
            );

          return builder;
        }
      );
  }

  private static IEnumerable<Type> RecursivelyFindPostgresqlEnumTypes(Type type)
  {
    if (type.GetCustomAttribute<PostgresqlEnumAttribute>() is not null)
    {
      yield return type;
    }

    if (type.IsGenericType)
    {
      foreach (var genericArgument in type.GetGenericArguments())
      {
        foreach (var nestedType in RecursivelyFindPostgresqlEnumTypes(
          genericArgument))
        {
          yield return nestedType;
        }
      }
    }
  }
}
