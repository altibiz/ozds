using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Extensions;

public static class DbContextTableExtensions
{
  public static string? GetTableNameFromEntityType(
    this DbContext context,
    Type entityType
  )
  {
    return context.Model.FindEntityType(entityType)?.GetTableName();
  }

  public static Type? GetEntityTypeFromTableName(
    this DbContext context,
    string tableName
  )
  {
    return context.Model
      .GetEntityTypes()
      .FirstOrDefault(entity => entity.GetTableName() == tableName)
      ?.ClrType;
  }

  public static string? GetEntityTypeNameFromEntityType(
    this DbContext context,
    Type entityType
  )
  {
    return context.Model.FindEntityType(entityType)?.ClrType.Name;
  }

  public static Type? GetEntityTypeFromEntityTypeName(
    this DbContext context,
    string entityTypeName
  )
  {
    return context.Model
      .GetEntityTypes()
      .FirstOrDefault(entity => entity.ClrType.Name == entityTypeName)
      ?.ClrType;
  }

  public static string? GetTableName<T>(this DbContext context)
  {
    return context.Model.FindEntityType(typeof(T))?.GetTableName();
  }

  public static IEnumerable<string> GetColumnNames<T>(
    this DbContext context
  )
  {
    return GetColumnNames(context, typeof(T));
  }

  public static string? GetColumnName<T>(
    this DbContext context,
    string propertyName
  )
  {
    return GetColumnName(context, typeof(T), propertyName);
  }

  public static IEnumerable<string> GetColumnNames(
    this DbContext context,
    Type type
  )
  {
    return context.Model.FindEntityType(type)
      ?.GetProperties()
      .Select(x => x.GetColumnName())
      ?? throw new InvalidOperationException(
        $"Entity type {type.Name} not found");
  }

  public static string? GetColumnName(
    this DbContext context,
    Type type,
    string propertyName
  )
  {
    return context.Model.FindEntityType(type)
      ?.FindProperty(propertyName)
      ?.GetColumnName();
  }

  public static string? GetPrimaryKeyColumnName<T>(
    this DbContext context
  )
  {
    return context.Model.FindEntityType(typeof(T))
      ?.FindPrimaryKey()
      ?.Properties
      .Select(x => x.GetColumnName())
      .FirstOrDefault();
  }

  public static string? GetForeignKeyColumnName<T>(
    this DbContext context,
    string navigationPropertyName
  )
  {
    return context.Model.FindEntityType(typeof(T))
      ?.FindNavigation(navigationPropertyName)
      ?.ForeignKey
      .Properties
      .Select(x => x.GetColumnName())
      .FirstOrDefault();
  }
}
