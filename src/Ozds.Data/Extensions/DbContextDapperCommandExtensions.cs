using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;

namespace Ozds.Data.Extensions;

// TODO: split apart the reader into arrays of objects based on primary keys

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
      var splits = SplitReader(reader, propertyMappings);
      var instance = new T();
      MapProperties(instance, splits);
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
    List<ReaderSplit> splits
  )
  {
    foreach (var split in splits)
    {
      if (split is EntityReaderSplit entitySplit)
      {
        var entityType = entitySplit.EntityMapping.EntityType;
        var discriminatorProperty = entityType
          .FindDiscriminatorProperty();
        var discriminatorColumnName = discriminatorProperty != null
          ? GetColumnNameForProperty(entityType, discriminatorProperty)
          : null;

        object? discriminatorValue = null;
        if (discriminatorColumnName != null)
        {
          discriminatorValue = entitySplit.Values[discriminatorColumnName];
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
          entitySplit.EntityMapping.ColumnMappings,
          entitySplit.Values
        );

        MapComplexProperties(
          entityInstance,
          entitySplit.EntityMapping.ComplexPropertyMappings,
          entitySplit.Values
        );

        entitySplit.EntityMapping.Property.SetValue(instance, entityInstance);
      }
      else if (split is ScalarReaderSplit scalarSplit)
      {
        var value = scalarSplit.Value;
        if (value == DBNull.Value)
        {
          continue;
        }

        var converted = ConvertValue(
          value,
          scalarSplit.ScalarMapping.Property.PropertyType);
        scalarSplit.ScalarMapping.Property.SetValue(instance, converted);
      }
    }
  }

  private static void MapComplexProperties(
    object instance,
    List<ComplexPropertyMapping> complexPropertyMappings,
    Dictionary<string, object> reader
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
    Dictionary<string, object> values
  )
  {
    foreach (var columnMapping in columnMappings)
    {
      var value = values[columnMapping.ColumnName];
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

  private static List<ReaderSplit> SplitReader(
    DbDataReader reader,
    List<PropertyMapping> propertyMappings
  )
  {
    var results = new List<ReaderSplit>();
    var nextSplit = 0;
    foreach (var (propertyMapping, nextPropertyMapping) in propertyMappings
      .Zip(propertyMappings.Skip(1).Append(null)))
    {
      var currentSplit = nextSplit;

      if (nextPropertyMapping is EntityPropertyMapping nextEntityMapping)
      {
        var primaryKeyColumnName = nextEntityMapping.EntityType.FindPrimaryKey()
          ?.Properties[0]
          ?.GetColumnName()
          ?? throw new InvalidOperationException(
            $"Primary key not found for entity"
            + nextEntityMapping.EntityType.ClrType.Name);
        for (var i = currentSplit; i < reader.FieldCount; i++)
        {
          if (reader.GetName(i) == primaryKeyColumnName)
          {
            nextSplit = i + 1;
            break;
          }
        }
      }
      else if (nextPropertyMapping is ScalarPropertyMapping nextScalarMapping)
      {
        var columnName = nextScalarMapping.ColumnName;
        for (var i = currentSplit; i < reader.FieldCount; i++)
        {
          if (reader.GetName(i) == columnName)
          {
            nextSplit = i + 1;
            break;
          }
        }
      }
      else
      {
        nextSplit = reader.FieldCount;
      }

      if (propertyMapping is EntityPropertyMapping entityMapping)
      {
        var values = new Dictionary<string, object>();
        for (var i = currentSplit; i < nextSplit; i++)
        {
          var value = reader[i];
          values.Add(reader.GetName(i), value);
        }

        results.Add(new EntityReaderSplit
        {
          EntityMapping = entityMapping,
          Values = values
        });
      }
      if (propertyMapping is ScalarPropertyMapping scalarMapping)
      {
        var value = reader[currentSplit];
        var columnName = reader.GetName(currentSplit);
        results.Add(new ScalarReaderSplit
        {
          ScalarMapping = scalarMapping,
          Value = value,
          ColumnName = columnName
        });
      }
    }
    return results;
  }

  private static object ConvertValue(object value, Type type)
  {
    if (type == typeof(DateTimeOffset))
    {
      return new DateTimeOffset(
        DateTime.SpecifyKind((DateTime)value, DateTimeKind.Utc));
    }

    if (type.IsGenericType
      && type.GetGenericTypeDefinition() == typeof(List<>)
      && type.GetGenericArguments().Single() is { } elementType)
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

#pragma warning disable S2094 // Classes should not be empty
  private abstract class ReaderSplit
#pragma warning restore S2094 // Classes should not be empty
  {
  }

  private sealed class EntityReaderSplit : ReaderSplit
  {
    public required EntityPropertyMapping EntityMapping { get; init; }

    public required Dictionary<string, object> Values { get; init; }
  }

  private sealed class ScalarReaderSplit : ReaderSplit
  {
    public required ScalarPropertyMapping ScalarMapping { get; init; }

    public required object Value { get; init; }

    public required string ColumnName { get; init; }
  }
}
