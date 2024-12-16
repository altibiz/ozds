using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
    params string[] propertyName
  )
  {
    return GetColumnName(context, typeof(T), propertyName);
  }

  public static IEnumerable<string> GetColumnNames(
    this DbContext context,
    Type type
  )
  {
    var entityType = context.Model.FindEntityType(type);
    if (entityType is null)
    {
      throw new InvalidOperationException(
        $"Entity type {type.Name} not found");
    }

    var storeObjectIdentifier = StoreObjectIdentifier
      .Create(entityType, StoreObjectType.Table);
    if (storeObjectIdentifier is null)
    {
      throw new InvalidOperationException(
        $"Store object identifier for entity type {type.Name} not found");
    }

    return entityType
      .GetProperties()
      .Select(x => x.GetColumnName())
      .Concat(entityType
        .GetComplexProperties()
        .SelectMany(x => x.ComplexType
          .GetProperties()
          .Select(x => x.GetColumnName(storeObjectIdentifier.Value)
            ?? throw new InvalidOperationException(
              $"Column name not found for property {x.Name} in type {type.Name}"))));
  }

  public static string? GetColumnName(
    this DbContext context,
    Type type,
    params string[] propertyNames
  )
  {
    string? GetColumnName(
      ITypeBase type,
      StoreObjectIdentifier storeObjectIdentifier,
      params string[] propertyNames
    )
    {
      if (propertyNames.Length == 0)
      {
        throw new ArgumentException(
          "Property name is required.");
      }

      if (propertyNames.Length == 1)
      {
        return type
          ?.FindProperty(propertyNames[0])
          ?.GetColumnName();
      }

      var complexType = type.GetComplexProperties()
        .FirstOrDefault(x => x.Name == propertyNames[0]);
      if (complexType is null)
      {
        throw new InvalidOperationException(
          $"No complex type found for property {propertyNames[0]} on type {type.Name}");
      }

      return GetColumnName(
        complexType.ComplexType,
        storeObjectIdentifier,
        propertyNames.Skip(1).ToArray()
      );
    }

    var entityType = context.Model.FindEntityType(type);
    if (entityType is null)
    {
      throw new InvalidOperationException(
        $"Entity type {type.Name} not found");
    }

    var storeObjectIdentifier = StoreObjectIdentifier
      .Create(entityType, StoreObjectType.Table);
    if (storeObjectIdentifier is null)
    {
      throw new InvalidOperationException(
        $"Store object identifier for entity type {type.Name} not found");
    }

    return GetColumnName(
      entityType,
      storeObjectIdentifier.Value,
      propertyNames);
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
