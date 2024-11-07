using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Extensions;

public static class DataDbContextDapperNaiveCommandExtensions
{
#pragma warning disable S1133 // Deprecated code should be removed
  [Obsolete("Use DapperCommand instead")]
#pragma warning restore S1133 // Deprecated code should be removed
  public static async Task<List<T>> NaiveDapperCommand<T>(
    this DbContext context,
    string sql,
    CancellationToken cancellationToken,
    object? parameters = null
  )
    where T : class
  {
    var connection = context.GetDapperDbConnection();
    var command = new CommandDefinition(
      sql,
      parameters,
      cancellationToken: cancellationToken
    );

    var results = await (MultiMapAsync
      .MakeGenericMethod(typeof(T))
      .Invoke(
        null,
        new object[]
        {
          connection,
          command,
          context.GetDapperTypes(typeof(T)),
          context.GetDapperMap<T>(),
          context.GetDapperSplitOn(typeof(T))
        }) as Task<IEnumerable<T>>)!;

    return results.ToList();
  }

#pragma warning disable IDE0060 // Remove unused parameter
  private static Type[] GetDapperTypes(this DbContext context, Type type)
#pragma warning restore IDE0060 // Remove unused parameter
  {
    return type
      .GetProperties(BindingFlags.Public | BindingFlags.Instance)
      .Select(property => property.PropertyType)
      .ToArray();
  }

  private static Func<object[], T> GetDapperMap<T>(
    this DbContext context
  )
    where T : class
  {
    return objects =>
    {
      var instance = Activator.CreateInstance(typeof(T))
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {typeof(T).Name}");

      foreach (var (property, @object) in typeof(T)
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Zip(objects))
      {
        property.SetValue(instance, @object);
      }

      return instance as T
        ?? throw new InvalidOperationException(
          $"Failed to cast instance to type {typeof(T).Name}");
    };
  }

  private static string GetDapperSplitOn(this DbContext context, Type type)
  {
    return string.Join(
      ",",
      type
        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Select(property =>
        {
          var entityType = context.Model.FindEntityType(property.PropertyType)
            ?? throw new InvalidOperationException(
              $"Entity type not found for {property.PropertyType.Name}");

          var primaryKey = entityType.FindPrimaryKey()?.Properties[0]
            ?? throw new InvalidOperationException(
              $"Primary key not found for entity {entityType.ClrType.Name}");

          var columnName = primaryKey.GetColumnName();

          return columnName;
        })
    );
  }

  private static readonly MethodInfo MultiMapAsync =
    typeof(SqlMapper)
      .GetMethods(
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
        BindingFlags.Static | BindingFlags.NonPublic
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
      )
      .FirstOrDefault(method =>
        method.Name == "MultiMapAsync" &&
        method.GetGenericArguments().Length == 1)
      ?? throw new InvalidOperationException("MultiMapAsync doesn't exist");
}
