using System.Collections.Concurrent;
using System.Data;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

public static class DbContextDapperCommandExtensions
{
  private static readonly ConcurrentDictionary<Type, List<PropertyMapping>>
    _propertyMappingsCache = new();

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
    using var reader = await connection.ExecuteReaderAsync(command);

    var results = new List<T>();
    var propertyMappings = _propertyMappingsCache.GetOrAdd(
      typeof(T),
      (_) => GetPropertyMappings(context, typeof(T)));
    while (await reader.ReadAsync(cancellationToken))
    {
      var instance = new T();
      MapProperties(instance, propertyMappings, reader);
      results.Add(instance);
    }

    return results;
  }

  private static List<PropertyMapping> GetPropertyMappings(
    DbContext context,
    Type clrType
  )
  {
    var propertyMappings = new List<PropertyMapping>();

    var properties = clrType
      .GetProperties(BindingFlags.Public | BindingFlags.Instance);
    foreach (var property in properties)
    {
      var efType = context.Model.FindEntityType(property.PropertyType);
      if (efType is { } entityType)
      {
        var columnMappings = GetColumnMappings(efType, efType);
        var complexPropertyMappings = new List<ComplexPropertyMapping>();

        foreach (var efProperty in entityType.GetComplexProperties())
        {
          var mapping = GetComplexPropertyMapping(efType, efProperty);
          complexPropertyMappings.Add(mapping);
        }

        var entityPropertyMapping = new EntityPropertyMapping
        {
          EntityType = entityType,
          ComplexPropertyMappings = complexPropertyMappings,
          ColumnMappings = columnMappings,
          Property = property
        };

        propertyMappings.Add(entityPropertyMapping);
      }
      else
      {
        var columnName = property.Name.ToSnakeCase();
        var scalarPropertyMapping = new ScalarPropertyMapping
        {
          Property = property,
          ColumnName = columnName
        };
        propertyMappings.Add(scalarPropertyMapping);
      }
    }

    return propertyMappings;
  }

  private static ComplexPropertyMapping GetComplexPropertyMapping(
    ITypeBase tableType,
    IComplexProperty complexProperty
  )
  {
    var columnMappings = GetColumnMappings(tableType, complexProperty.ComplexType);
    var complexPropertyMappings = new List<ComplexPropertyMapping>();

    foreach (var efProperty in complexProperty.ComplexType.GetComplexProperties())
    {
      var complexPropertyMapping = GetComplexPropertyMapping(tableType, efProperty);
      complexPropertyMappings.Add(complexPropertyMapping);
    }

    return new ComplexPropertyMapping
    {
      ComplexProperty = complexProperty,
      ColumnMappings = columnMappings,
      ComplexPropertyMappings = complexPropertyMappings
    };
  }

  private static List<ColumnMapping> GetColumnMappings(
    ITypeBase tableType,
    ITypeBase type
  )
  {
    var columnMappings = new List<ColumnMapping>();

    foreach (var efProperty in type.GetProperties())
    {
      var columnName = GetColumnNameForProperty(tableType, efProperty);
      if (efProperty.PropertyInfo is { } property)
      {
        columnMappings.Add(new PropertyColumnMapping
        {
          Property = property,
          ColumnName = columnName
        });

        continue;
      }
      if (efProperty.FieldInfo is { } field)
      {
        columnMappings.Add(new FieldColumnMapping
        {
          Field = field,
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
        var entityType = entityMapping.EntityType;
        var discriminatorProperty = entityType
          .FindDiscriminatorProperty();
        var discriminatorColumnName = discriminatorProperty != null
          ? GetColumnNameForProperty(entityType, discriminatorProperty)
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

        MapColumns(
          entityInstance,
          entityMapping.ColumnMappings,
          reader
        );

        MapComplexProperties(
          entityInstance,
          entityMapping.ComplexPropertyMappings,
          reader
        );

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
  }

  private static void MapComplexProperties(
    object instance,
    List<ComplexPropertyMapping> complexPropertyMappings,
    IDataRecord reader
  )
  {
    foreach (var complexMapping in complexPropertyMappings)
    {
      var complexType = complexMapping.ComplexProperty.ComplexType.ClrType;
      var complexInstance = Activator.CreateInstance(complexType)
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {complexType.Name}");

      MapColumns(complexInstance, complexMapping.ColumnMappings, reader);

      MapComplexProperties(
        complexInstance,
        complexMapping.ComplexPropertyMappings,
        reader
      );

      if (complexMapping.ComplexProperty.PropertyInfo is { } property)
      {
        property.SetValue(instance, complexInstance);
      }
      if (complexMapping.ComplexProperty.FieldInfo is { } field)
      {
        field.SetValue(instance, complexInstance);
      }
    }
  }

  private static void MapColumns(
    object instance,
    IEnumerable<ColumnMapping> columnMappings,
    IDataRecord reader
  )
  {
    foreach (var columnMapping in columnMappings)
    {
      var value = reader[columnMapping.ColumnName];
      if (value == DBNull.Value)
      {
        continue;
      }

      if (columnMapping is PropertyColumnMapping propertyMapping)
      {
        var converted =
          ConvertValue(value, propertyMapping.Property.PropertyType);
        propertyMapping.Property.SetValue(instance, converted);
      }
      if (columnMapping is FieldColumnMapping fieldMapping)
      {
        var converted =
          ConvertValue(value, fieldMapping.Field.FieldType);
        fieldMapping.Field.SetValue(instance, converted);
      }
    }
  }

  private static object ConvertValue(object value, Type type)
  {
    if (value is DateTime dateTime)
    {
      return new DateTimeOffset(
        DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
    }

    if (value.GetType().IsArray
      && value.GetType().GetElementType() is Type elementType)
    {
      return typeof(Enumerable)
        .GetMethod(nameof(Enumerable.ToList))!
        .MakeGenericMethod(elementType)!
        .Invoke(null, [value])!;
    }

    if (type.IsAssignableTo(typeof(IConvertible)))
    {
      return Convert.ChangeType(value, type)!;
    }

    return value;
  }

  private static string GetColumnNameForProperty(
    ITypeBase tableType,
    IProperty property
  )
  {
    var storeObjectIdentifier = StoreObjectIdentifier
      .Create(tableType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"Unable to determine store object identifier"
        + $" for type {tableType.ClrType.Name}");

    return property.GetColumnName(storeObjectIdentifier)
      ?? throw new InvalidOperationException(
        $"Column name not found for property"
        + $" {property.Name} in type {tableType.ClrType.Name}");
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

    public required List<ComplexPropertyMapping> ComplexPropertyMappings
    {
      get;
      init;
    }
  }

  private sealed class ScalarPropertyMapping : PropertyMapping
  {
    public required string ColumnName { get; init; }
  }

  private sealed class ComplexPropertyMapping
  {
    public required IComplexProperty ComplexProperty { get; init; }

    public required List<ColumnMapping> ColumnMappings { get; init; }

    public required List<ComplexPropertyMapping> ComplexPropertyMappings
    {
      get;
      init;
    }
  }

  private abstract class ColumnMapping
  {
    public required string ColumnName { get; init; }
  }

  private sealed class PropertyColumnMapping : ColumnMapping
  {
    public required PropertyInfo Property { get; init; }
  }

  private sealed class FieldColumnMapping : ColumnMapping
  {
    public required FieldInfo Field { get; init; }
  }
}
