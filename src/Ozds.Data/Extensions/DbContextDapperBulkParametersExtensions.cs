using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

public record RowWithIndex(
  object Row,
  int Index
);

public static class DbContextDapperBulkParametersExtensions
{
  public static Dictionary<string, object> CreateBulkParameters(
    this DbContext context,
    IEnumerable<RowWithIndex> rowsWithIndices
  )
  {
    var parameters = new Dictionary<string, object>();
    foreach (var (row, index) in rowsWithIndices)
    {
      var entityType = context.Model.FindEntityType(row.GetType())
        ?? throw new InvalidOperationException(
          $"No entity type found for {row.GetType()}.");
      var storeObjectIdentifier = StoreObjectIdentifier
          .Create(entityType, StoreObjectType.Table)
        ?? throw new InvalidOperationException(
          $"No store object identifier found for {row.GetType()}.");

      foreach (var (property, columnName, value) in entityType
        .GetPropertyValues(storeObjectIdentifier, row))
      {
        var parameterName = $"@p{index}_{columnName}";
        if (columnName is null)
        {
          continue;
        }

        var updated = value;
        if (value is { } enumValue && property.ClrType.IsEnum)
        {
          updated = enumValue.ToString()?.ToSnakeCase();
        }

        if (updated is not null)
        {
          parameters.Add(parameterName, updated);
        }
        else
        {
          parameters.Add(parameterName, DBNull.Value);
        }
      }
    }

    return parameters;
  }

  public static List<Dictionary<string, string>> CreateBulkArguments(
    this DbContext context,
    IEnumerable<RowWithIndex> rowsWithIndices
  )
  {
    var arguments = new Dictionary<int, Dictionary<string, string>>();
    foreach (var (row, index) in rowsWithIndices)
    {
      var entityType = context.Model.FindEntityType(row.GetType())
        ?? throw new InvalidOperationException(
          $"No entity type found for {row.GetType()}.");
      var storeObjectIdentifier = StoreObjectIdentifier
          .Create(entityType, StoreObjectType.Table)
        ?? throw new InvalidOperationException(
          $"No store object identifier found for {row.GetType()}.");

      var propertyValues = entityType
        .GetPropertyValues(storeObjectIdentifier, row);

      foreach (var (property, columnName, value) in propertyValues)
      {
        var parameterName = $"@p{index}_{columnName}";
        if (arguments.TryGetValue(index, out var argumentsList))
        {
          argumentsList.Add(columnName, parameterName);
        }
        else
        {
          argumentsList = new Dictionary<string, string>();
          argumentsList.Add(columnName, parameterName);
          arguments.Add(index, argumentsList);
        }
      }
    }

    return arguments.Select(x => x.Value).ToList();
  }

  public static JsonDocument CreateBulkJsonParameter(
    this DbContext context,
    IEnumerable<object> rows
  )
  {
    var parameters = new List<Dictionary<string, object?>>();
    foreach (var row in rows)
    {
      parameters.Add(GetRowParameters(context, row));
    }

    return JsonSerializer.SerializeToDocument(parameters);
  }

  public static async Task<JsonDocument> CreateBulkJsonParameter(
    this DbContext context,
    IAsyncEnumerable<object> rows,
    CancellationToken cancellationToken
  )
  {
    var parameters = new List<Dictionary<string, object?>>();
    await foreach (var row in rows.WithCancellation(cancellationToken))
    {
      parameters.Add(GetRowParameters(context, row));
    }

    return JsonSerializer.SerializeToDocument(parameters);
  }

  private static Dictionary<string, object?> GetRowParameters(
    DbContext context,
    object row
  )
  {
    var entityType = context.Model.FindEntityType(row.GetType())
      ?? throw new InvalidOperationException(
        $"No entity type found for {row.GetType()}.");

    var storeObjectIdentifier = StoreObjectIdentifier
        .Create(entityType, StoreObjectType.Table)
      ?? throw new InvalidOperationException(
        $"No store object identifier found for {row.GetType()}.");

    var rowParameters = new Dictionary<string, object?>();
    foreach (var (_, columnName, value) in entityType
      .GetPropertyValues(storeObjectIdentifier, row))
    {
      if (columnName is null)
      {
        continue;
      }

      if (value is Enum enumValue)
      {
        rowParameters.Add(columnName, enumValue.ToString()?.ToSnakeCase());
      }
      else
      {
        rowParameters.Add(columnName, value);
      }
    }

    return rowParameters;
  }

  private static IEnumerable<PropertyValue> GetPropertyValues(
    this ITypeBase type,
    StoreObjectIdentifier storeObjectIdentifier,
    object? row
  )
  {
    static IEnumerable<PropertyValue> Recursive(
      ITypeBase type,
      StoreObjectIdentifier storeObjectIdentifier,
      object? row
    )
    {
      foreach (var property in type
        .GetProperties()
        .OrderBy(x => x.GetIndex()))
      {
        yield return new PropertyValue(
          property,
          property.GetColumnName(storeObjectIdentifier)
          ?? throw new InvalidOperationException(
            $"No column name found for {property.Name} in {type.Name}."),
          property.PropertyInfo?.GetValue(row)
          ?? property.FieldInfo?.GetValue(row));
      }

      foreach (var complexProperty in type
        .GetComplexProperties()
        .OrderBy(x => x.GetIndex()))
      {
        var complexRow = complexProperty.PropertyInfo?.GetValue(row)
          ?? complexProperty.FieldInfo?.GetValue(row);
        foreach (var property in Recursive(
          complexProperty.ComplexType,
          storeObjectIdentifier,
          complexRow))
        {
          yield return property;
        }
      }
    }

    return Recursive(type, storeObjectIdentifier, row)
      .OrderBy(x => x.Property.GetIndex());
  }

  private sealed record PropertyValue(
    IProperty Property,
    string Column,
    object? Value
  );
}
