using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Ozds.Data.Attributes;

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
  {
    var objects = await DapperCommand(
      context,
      typeof(T),
      sql,
      cancellationToken,
      parameters
    );

    return objects.Select(x => (T)x).ToList();
  }

  [DapperResult]
  private sealed class DapperSingleResult<T>
  {
#pragma warning disable S1144 // Unused private types or members should be removed
    public required T Value { get; init; }
#pragma warning restore S1144 // Unused private types or members should be removed
  }

  public static async Task<List<object>> DapperCommand(
      this DbContext context,
      Type type,
      string sql,
      CancellationToken cancellationToken,
      object? parameters = null
  )
  {
    var resultAttribute = type.GetCustomAttribute<DapperResultAttribute>();
    if (resultAttribute is null)
    {
      type = typeof(DapperSingleResult<>).MakeGenericType(type);
    }

    var connection = context.GetDapperDbConnection();
    var command = new CommandDefinition(
        sql,
        parameters,
        transaction: context.Database.CurrentTransaction?.GetDbTransaction(),
        cancellationToken: cancellationToken
    );
    using var reader = await connection.ExecuteReaderAsync(command);

    var results = new List<object>();
    var propertyMappings = _propertyMappingsCache.GetOrAdd(
      type,
      (type) => GetPropertyMappings(context, type));
    while (await reader.ReadAsync(cancellationToken))
    {
      var splits = SplitReader(reader, propertyMappings);
      var instance = Activator.CreateInstance(type)
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {type.Name}");
      MapProperties(context, instance, splits);
      results.Add(instance);
    }

    if (resultAttribute is null)
    {
      return results
        .Select(x => type.GetProperty("Value")?.GetValue(x)!)
        .ToList();
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
        var complexPropertyMappings = GetComplexPropertyMappings(context, efType, efType);

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

  private static List<ComplexPropertyMapping> GetComplexPropertyMappings(
    DbContext context,
    ITypeBase tableType,
    ITypeBase type
  )
  {
    var complexPropertyMappings = new List<ComplexPropertyMapping>();

    foreach (var efProperty in type.GetComplexProperties())
    {
      var complexPropertyMapping =
        GetComplexPropertyMapping(context, tableType, efProperty);
      complexPropertyMappings.Add(complexPropertyMapping);
    }

    return complexPropertyMappings;
  }

  private static ComplexPropertyMapping GetComplexPropertyMapping(
    DbContext context,
    ITypeBase tableType,
    IComplexProperty complexProperty
  )
  {
    var correspondingEntityType = context.Model
      .FindEntityType(complexProperty.ComplexType.ClrType);

    var columnMappings = GetColumnMappings(
      tableType,
      complexProperty.ComplexType
    );
    var complexPropertyMappings = new List<ComplexPropertyMapping>();

    foreach (var efProperty in complexProperty.ComplexType.GetComplexProperties())
    {
      var complexPropertyMapping =
        GetComplexPropertyMapping(context, tableType, efProperty);
      complexPropertyMappings.Add(complexPropertyMapping);
    }

    return new ComplexPropertyMapping
    {
      ComplexProperty = complexProperty,
      CorrespondingEntityType = correspondingEntityType,
      TableType = tableType,
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
    DbContext context,
    object instance,
    List<ReaderSplit> splits
  )
  {
    foreach (var split in splits)
    {
      if (split is EntityReaderSplit entitySplit)
      {
        if (entitySplit.Values.All(x => x.Value == DBNull.Value))
        {
          continue;
        }

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

        var finalType = entityType;
        var finalColumnMappings = entitySplit.EntityMapping.ColumnMappings;
        var finalComplexPropertyMappings =
          entitySplit.EntityMapping.ComplexPropertyMappings;
        if (discriminatorValue is { } && discriminatorValue != DBNull.Value)
        {
          finalType = GetConcreteEntityType(
            entityType,
            discriminatorValue
          );
          finalColumnMappings = GetColumnMappings(
            finalType,
            finalType
          );
          finalComplexPropertyMappings = GetComplexPropertyMappings(
            context,
            finalType,
            finalType
          );
        }

        var entityInstance = Activator.CreateInstance(finalType.ClrType)
          ?? throw new InvalidOperationException(
            $"Failed to create instance of {finalType.Name}");

        MapColumns(
          entityInstance,
          finalColumnMappings,
          entitySplit.Values
        );

        MapComplexProperties(
          context,
          entityInstance,
          finalComplexPropertyMappings,
          entitySplit.Values
        );

        entitySplit.EntityMapping.Property.SetValue(instance, entityInstance);
      }
      else if (split is ScalarReaderSplit scalarSplit)
      {
        if (scalarSplit.Value == DBNull.Value)
        {
          continue;
        }

        var converted = ConvertValue(
          scalarSplit.Value,
          scalarSplit.ScalarMapping.Property.PropertyType);
        scalarSplit.ScalarMapping.Property.SetValue(instance, converted);
      }
    }
  }

  private static void MapComplexProperties(
    DbContext context,
    object instance,
    List<ComplexPropertyMapping> complexPropertyMappings,
    Dictionary<string, object> reader
  )
  {
    foreach (var complexMapping in complexPropertyMappings)
    {
      var correspondingEntityType = complexMapping.CorrespondingEntityType;
      var discriminatorProperty = correspondingEntityType
        ?.FindDiscriminatorProperty();
      var discriminatorColumnName = discriminatorProperty != null
        ? GetColumnNameForProperty(
            complexMapping.TableType,
            complexMapping.ComplexProperty.ComplexType
              .GetProperty(discriminatorProperty.Name))
        : null;

      object? discriminatorValue = null;
      if (discriminatorColumnName != null)
      {
        discriminatorValue = reader[discriminatorColumnName];
      }

      var finalType = (ITypeBase)complexMapping.ComplexProperty.ComplexType;
      var finalColumnMappings = complexMapping.ColumnMappings;
      var finalComplexPropertyMappings = complexMapping.ComplexPropertyMappings;
      if (correspondingEntityType is { }
        && discriminatorValue is { }
        && !string.IsNullOrEmpty(discriminatorValue.ToString()))
      {
        var entityType = GetConcreteEntityType(
          correspondingEntityType,
          discriminatorValue
        );
        if (entityType.ClrType != finalType.ClrType)
        {
          throw new InvalidOperationException(
            $"No mapping for discriminator value '{discriminatorValue}'"
            + $" on entity type '{entityType.Name}'"
            + $" to type '{finalType.Name}'.");
        }
      }

      var complexInstance = Activator.CreateInstance(finalType.ClrType)
        ?? throw new InvalidOperationException(
          $"Failed to create instance of {finalType.ClrType.Name}");

      MapColumns(complexInstance, finalColumnMappings, reader);

      MapComplexProperties(
        context,
        complexInstance,
        finalComplexPropertyMappings,
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
    object discriminatorValue
  )
  {
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

    var splitColumns = new Queue<string>(propertyMappings
      .Select(mapping =>
      {
        var splitColumn = mapping switch
        {
          EntityPropertyMapping entityMapping => entityMapping
            .EntityType.FindPrimaryKey()
              ?.Properties[0]
              ?.GetColumnName()
              ?? throw new InvalidOperationException(
                $"Primary key not found for entity"
                + entityMapping.EntityType.ClrType.Name),
          ScalarPropertyMapping scalarMapping => scalarMapping.Property.Name,
          _ => throw new InvalidOperationException(
            $"Unknown property mapping type: {mapping.GetType().Name}")
        };
        return splitColumn;
      }));

    var splits = Enumerable.Range(0, reader.FieldCount)
      .Aggregate(new List<int>(), (splits, i) =>
      {
        if (splitColumns.TryPeek(out var splitColumn)
          && reader.GetName(i) == splitColumn)
        {
          splits.Add(i);
          splitColumns.Dequeue();
        }

        return splits;
      });

    if (splitColumns.Count > 0)
    {
      throw new InvalidOperationException(
        $"Unable to find split column for {splitColumns.Peek()}");
    }

    foreach (var ((mapping, splitStart), splitEnd) in propertyMappings
      .Zip(splits)
      .Zip(splits.Skip(1).Append(reader.FieldCount)))
    {
      if (mapping is EntityPropertyMapping entityMapping)
      {
        var values = new Dictionary<string, object>();
        for (var i = splitStart; i < splitEnd; i++)
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
      if (mapping is ScalarPropertyMapping scalarMapping)
      {
        var value = reader[splitStart];
        var columnName = reader.GetName(splitStart);
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

  private static object? ConvertValue(object value, Type type)
  {
    if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
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

    public required IEntityType? CorrespondingEntityType { get; init; }

    public required ITypeBase TableType { get; init; }

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
