using System.Data;
using System.Data.Common;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace Ozds.Data.Extensions;

public static class DataDbContextDapperExtensions
{
  public static async Task<List<T>> DapperCommand<T>(
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

    var results = await (typeof(SqlMapper)
      .GetMethod(
        "MultiMapAsync",
        // NOTE: fuck you Dapper
#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
        BindingFlags.Static | BindingFlags.NonPublic)!
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
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

  private static DbConnection GetDapperDbConnection(this DbContext context)
  {
    _lock.Wait();

    if (_isRegistered)
    {
      _lock.Release();
      return context.Database.GetDbConnection();
    }

    _isRegistered = true;

    foreach (var type in context.Model.GetEntityTypes())
    {
      SetTypeMap(
        type.ClrType,
        new CustomPropertyTypeMap(
          type.ClrType,
          (type, columnName) =>
          {
            var propertyName = GetMappedPropertyName(context, type, columnName)
              ?? throw new InvalidOperationException(
                $"Property name doesn't exist for {type.Name}.{columnName}");
            return type.GetProperty(propertyName)
              ?? throw new InvalidOperationException(
                $"Property {type.Name}.{propertyName} doesn't exist");
          }));
    }

    _lock.Release();

    return context.Database.GetDbConnection();
  }

  private static Type[] GetDapperTypes(this DbContext context, Type type)
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

  private static string? GetMappedPropertyName(
    DbContext context,
    Type entityType,
    string columnName
  )
  {
    var entityTypeModel = context.Model.FindEntityType(entityType);
    var property = entityTypeModel
      ?.GetProperties()
      .FirstOrDefault(property => property
        .GetColumnName()
        .Equals(columnName, StringComparison.OrdinalIgnoreCase));
    return property?.Name;
  }

  private static readonly SemaphoreSlim _lock = new(1, 1);

  private static bool _isRegistered = false;
}
