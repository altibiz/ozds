using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

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

    var propertyMappings = GetPropertyMappings(typeof(T), context, null);
    using var reader = await connection.ExecuteReaderAsync(command);
    while (await reader.ReadAsync(cancellationToken))
    {
      var instance = Activator.CreateInstance(typeof(T))
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {typeof(T).Name}");

      MapProperties(instance, propertyMappings, reader);

      results.Add((T)instance);
    }

    return results;
  }

  private static List<PropertyMapping> GetPropertyMappings(
    Type clrType,
    DbContext context,
    ITypeBase? parentTypeBase
  )
  {
    var propertyMappings = new List<PropertyMapping>();

    ITypeBase typeBase = parentTypeBase
      ?? context.Model.FindEntityType(clrType)
      ?? throw new InvalidOperationException(
        $"Type {clrType.Name} not found in model");

    var properties = clrType
      .GetProperties(BindingFlags.Public | BindingFlags.Instance);

    foreach (var property in properties)
    {
      var propertyType = property.PropertyType;

      if (typeBase is IEntityType entityType)
      {
        var propertyBase = entityType.FindProperty(property.Name);
        if (propertyBase != null)
        {
          // It's a scalar property
          var columnName = GetColumnNameForProperty(propertyBase, entityType);

          propertyMappings.Add(new ScalarPropertyMapping
          {
            Property = property,
            ColumnName = columnName
          });

          continue;
        }

        // Check if it's a complex property
        var complexProperty = entityType.FindComplexProperty(property.Name);
        if (complexProperty != null)
        {
          var complexPropertyMapping = GetComplexPropertyMapping(
            complexProperty,
            context
          );

          propertyMappings.Add(new ComplexPropertyMapping
          {
            Property = complexProperty,
            ColumnMappings = complexPropertyMapping.ColumnMappings,
            ComplexPropertyMappings = complexPropertyMapping.ComplexPropertyMappings
          });

          continue;
        }
      }
      else if (typeBase is IComplexType complexType)
      {
        var propertyBase = complexType.FindProperty(property.Name);
        if (propertyBase != null)
        {
          // It's a scalar property in complex type
          var columnName = GetColumnNameForProperty(propertyBase, complexType);

          propertyMappings.Add(new ScalarPropertyMapping
          {
            Property = property,
            ColumnName = columnName
          });

          continue;
        }

        // Check if it's a nested complex property
        var nestedComplexProperty = complexType.FindComplexProperty(property.Name);
        if (nestedComplexProperty != null)
        {
          var complexPropertyMapping = GetComplexPropertyMapping(
            nestedComplexProperty,
            context
          );

          propertyMappings.Add(new ComplexPropertyMapping
          {
            Property = nestedComplexProperty,
            ColumnMappings = complexPropertyMapping.ColumnMappings,
            ComplexPropertyMappings = complexPropertyMapping.ComplexPropertyMappings
          });

          continue;
        }
      }

      throw new InvalidOperationException(
        $"Property {property.Name} not found in type {typeBase.ClrType.Name}");
    }

    return propertyMappings;
  }

  private static ComplexPropertyMapping GetComplexPropertyMapping(
    IComplexProperty complexProperty,
    DbContext context
  )
  {
    var columnMappings = new List<ColumnMapping>();
    var complexPropertyMappings = new List<ComplexPropertyMapping>();

    var complexType = complexProperty.ComplexType;
    var properties = complexType.ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

    foreach (var property in properties)
    {
      var propertyType = property.PropertyType;

      var propertyBase = complexType.FindProperty(property.Name);
      if (propertyBase != null)
      {
        // It's a scalar property
        var columnName = GetColumnNameForProperty(propertyBase, complexType);

        columnMappings.Add(new ColumnMapping
        {
          PropertyInfo = property,
          ColumnName = columnName
        });

        continue;
      }

      // Check if it's a nested complex property
      var nestedComplexProperty = complexType.FindComplexProperty(property.Name);
      if (nestedComplexProperty != null)
      {
        var nestedMapping = GetComplexPropertyMapping(nestedComplexProperty, context);

        complexPropertyMappings.Add(nestedMapping);

        continue;
      }

      throw new InvalidOperationException(
        $"Property {property.Name} not found in complex type {complexType.ClrType.Name}");
    }

    return new ComplexPropertyMapping
    {
      Property = complexProperty,
      ColumnMappings = columnMappings,
      ComplexPropertyMappings = complexPropertyMappings
    };
  }

  private static List<ColumnMapping> GetColumnMappings(
    IEntityType entityType
  )
  {
    var columnMappings = new List<ColumnMapping>();
    var properties = entityType.ClrType
      .GetProperties(BindingFlags.Public | BindingFlags.Instance);

    var storeObjectIdentifier = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"Unable to determine store object identifier for entity type {entityType.ClrType.Name}");

    foreach (var property in properties)
    {
      var efProperty = entityType.FindProperty(property.Name);
      if (efProperty != null)
      {
        var columnName = efProperty.GetColumnName(storeObjectIdentifier)
          ?? throw new InvalidOperationException(
            $"Column name not found for property {property.Name} in entity {entityType.ClrType.Name}");

        columnMappings.Add(new ColumnMapping
        {
          PropertyInfo = property,
          ColumnName = columnName
        });
      }
    }

    return columnMappings;
  }

  private static void MapProperties(
    object instance,
    List<PropertyMapping> propertyMappings,
    IDataRecord reader
  )
  {
    foreach (var mapping in propertyMappings)
    {
      if (mapping is EntityPropertyMapping entityMapping)
      {
        var entityColumns = entityMapping.ColumnMappings;
        var entityType = entityMapping.EntityType;
        var discriminatorProperty = entityMapping.DiscriminatorProperty;
        var discriminatorColumnName = discriminatorProperty != null
          ? GetColumnNameForProperty(discriminatorProperty, entityType)
          : null;

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

        // Map complex properties of the entity
        MapComplexProperties(entityInstance, entityMapping.ComplexPropertyMappings, reader);

        mapping.Property.SetValue(instance, entityInstance);
      }
      else if (mapping is ComplexPropertyMapping complexMapping)
      {
        var complexType = complexMapping.Property.ComplexType.ClrType;
        var complexInstance = Activator.CreateInstance(complexType)
          ?? throw new InvalidOperationException(
            $"Failed to create instance of {complexType.Name}");

        // Map scalar properties
        foreach (var columnMapping in complexMapping.ColumnMappings)
        {
          var value = reader[columnMapping.ColumnName];
          if (value != DBNull.Value)
          {
            columnMapping.PropertyInfo.SetValue(complexInstance, value);
          }
        }

        // Map nested complex properties
        MapComplexProperties(complexInstance, complexMapping.ComplexPropertyMappings, reader);

        if (mapping is PropertyMapping propertyMapping)
        {
          propertyMapping.Property.SetValue(instance, complexInstance);
        }
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
  }

  private static void MapComplexProperties(
    object instance,
    List<ComplexPropertyMapping> complexPropertyMappings,
    IDataRecord reader
  )
  {
    foreach (var complexMapping in complexPropertyMappings)
    {
      var complexType = complexMapping.Property.ComplexType.ClrType;
      var complexInstance = Activator.CreateInstance(complexType)
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {complexType.Name}");

      // Map scalar properties
      foreach (var columnMapping in complexMapping.ColumnMappings)
      {
        var value = reader[columnMapping.ColumnName];
        if (value != DBNull.Value)
        {
          columnMapping.PropertyInfo.SetValue(complexInstance, value);
        }
      }

      // Map nested complex properties
      MapComplexProperties(complexInstance, complexMapping.ComplexPropertyMappings, reader);

      var propertyInfo = complexMapping.Property.PropertyInfo;
      propertyInfo.SetValue(instance, complexInstance);
    }
  }

  private static string GetColumnNameForProperty(
    IPropertyBase property,
    ITypeBase typeBase
  )
  {
    var storeObjectIdentifier = StoreObjectIdentifier.Create(typeBase, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"Unable to determine store object identifier for type {typeBase.ClrType.Name}");

    return property.GetColumnName(storeObjectIdentifier)
      ?? throw new InvalidOperationException(
        $"Column name not found for property {property.Name} in type {typeBase.ClrType.Name}");
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

  private abstract class PropertyMapping
  {
    public required PropertyInfo Property { get; init; }
  }

  private sealed class EntityPropertyMapping : PropertyMapping
  {
    public required IEntityType EntityType { get; init; }
    public required List<ColumnMapping> ColumnMappings { get; init; }
    public required List<ComplexPropertyMapping> ComplexPropertyMappings { get; init; }
    public IProperty? DiscriminatorProperty { get; init; }
  }

  private sealed class ScalarPropertyMapping : PropertyMapping
  {
    public required string ColumnName { get; init; }
  }

  private sealed class ComplexPropertyMapping
  {
    public required IComplexProperty Property { get; init; }
    public required List<ColumnMapping> ColumnMappings { get; init; }
    public required List<ComplexPropertyMapping> ComplexPropertyMappings { get; init; }
  }

  private sealed class ColumnMapping
  {
    public required PropertyInfo PropertyInfo { get; init; }
    public required string ColumnName { get; init; }
  }
}
