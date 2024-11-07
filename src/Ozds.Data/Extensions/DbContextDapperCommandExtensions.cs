using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

// TODO: complex properties

public static class DbContextDapperCommandExtensions
{
  public static async Task<List<T>> DapperCommand<T>(
      this DbContext context,
      string sql,
      CancellationToken cancellationToken,
      object? parameters = null
  )
    where T : class, new()
  {
    var connection = context.GetDapperDbConnection();
    var command = new CommandDefinition(
        sql,
        parameters,
        cancellationToken: cancellationToken
    );

    var results = new List<T>();

    var propertyMappings = GetPropertyMappings<T>(context);
    using var reader = await connection.ExecuteReaderAsync(command);
    while (await reader.ReadAsync(cancellationToken))
    {
      var instance = new T();

      foreach (var mapping in propertyMappings)
      {
        if (mapping is EntityPropertyMapping entityMapping)
        {
          var entityColumns = entityMapping.ColumnMappings;
          var entityType = entityMapping.EntityType;
          var discriminatorProperty = entityMapping.DiscriminatorProperty;
          var discriminatorColumnName = discriminatorProperty?.GetColumnName();

          object? discriminatorValue = null;
          if (discriminatorColumnName != null)
          {
            discriminatorValue = reader[discriminatorColumnName];
          }

          var concreteEntityType = GetConcreteEntityType(
            entityType,
            discriminatorValue
          );

          var concreteType = concreteEntityType.ClrType;

          var entityInstance = Activator.CreateInstance(concreteType)
            ?? throw new InvalidOperationException(
              $"Failed to create instance of {concreteType.Name}");

          foreach (var columnMapping in entityColumns)
          {
            var value = reader[columnMapping.ColumnName];
            if (value != DBNull.Value)
            {
              columnMapping.PropertyInfo.SetValue(entityInstance, value);
            }
          }

          mapping.Property.SetValue(instance, entityInstance);
        }
        else if (mapping is ScalarPropertyMapping scalarMapping)
        {
          var value = reader[scalarMapping.ColumnName];
          if (value != DBNull.Value)
          {
            mapping.Property.SetValue(instance, value);
          }
        }
      }

      results.Add(instance);
    }

    return results;
  }

  private static List<PropertyMapping> GetPropertyMappings<T>(
    DbContext context
  )
  {
    var entityProperties = typeof(T)
      .GetProperties(BindingFlags.Public | BindingFlags.Instance);

    var propertyMappings = new List<PropertyMapping>();

    foreach (var property in entityProperties)
    {
      var propertyType = property.PropertyType;
      var entityType = context.Model.FindEntityType(propertyType);

      if (entityType != null)
      {
        var discriminatorProperty = entityType.FindDiscriminatorProperty();
        var columnMappings = GetColumnMappings(context, entityType);

        propertyMappings.Add(new EntityPropertyMapping
        {
          Property = property,
          EntityType = entityType,
          DiscriminatorProperty = discriminatorProperty,
          ColumnMappings = columnMappings
        });
      }
      else
      {
        var columnName = context.GetColumnName(typeof(T), property.Name);

        propertyMappings.Add(new ScalarPropertyMapping
        {
          Property = property,
          ColumnName = columnName
        });
      }
    }

    return propertyMappings;
  }

#pragma warning disable S1172 // Unused method parameters should be removed
  private static List<ColumnMapping> GetColumnMappings(
    DbContext context,
    IEntityType entityType
  )
#pragma warning restore S1172 // Unused method parameters should be removed
  {
    var columnMappings = new List<ColumnMapping>();
    var properties = entityType.ClrType
      .GetProperties(BindingFlags.Public | BindingFlags.Instance);

    foreach (var property in properties)
    {
      var efProperty = entityType.FindProperty(property.Name);
      if (efProperty != null)
      {
        var columnName = efProperty.GetColumnName();
        columnMappings.Add(new ColumnMapping
        {
          PropertyInfo = property,
          ColumnName = columnName
        });
      }
    }

    return columnMappings;
  }

  private static IEntityType GetConcreteEntityType(
    IEntityType baseEntityType,
    object? discriminatorValue
  )
  {
    if (discriminatorValue == null)
    {
      return baseEntityType;
    }

    var concreteEntityType = baseEntityType
      .GetDerivedTypesInclusive()
      .FirstOrDefault(e => Equals(
        e.GetDiscriminatorValue(),
        discriminatorValue))
      ?? throw new InvalidOperationException(
        $"Unknown discriminator value: {discriminatorValue}");

    return concreteEntityType;
  }

  private static string GetColumnName(
    this DbContext context,
    Type clrType,
    string propertyName
  )
  {
    var entityType = context.Model.FindEntityType(clrType)
      ?? throw new InvalidOperationException(
        $"Entity type {clrType.Name} not found in model");

    var property = entityType.FindProperty(propertyName)
      ?? throw new InvalidOperationException(
        $"Property {propertyName} not found in entity {clrType.Name}");

    return property.GetColumnName()
      ?? throw new InvalidOperationException(
        $"Column name not found for property"
        + $" {propertyName} in entity {clrType.Name}");
  }

  private class PropertyMapping
  {
    public required PropertyInfo Property { get; init; }
  }

  private sealed class EntityPropertyMapping : PropertyMapping
  {
    public required IEntityType EntityType { get; init; }
    public required List<ColumnMapping> ColumnMappings { get; init; }
    public IProperty? DiscriminatorProperty { get; init; }
  }

  private sealed class ScalarPropertyMapping : PropertyMapping
  {
    public required string ColumnName { get; init; }
  }

  private sealed class ColumnMapping
  {
    public required PropertyInfo PropertyInfo { get; init; }
    public required string ColumnName { get; init; }
  }
}
