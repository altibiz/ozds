using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

public static class DbContextDapperBulkParametersExtensions
{
  public static Dictionary<string, object> CreateBulkParameters(
    this DbContext context,
    IEnumerable<(object, int)> rowsWithIndices
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
        .GetProperties(storeObjectIdentifier, row))
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

  private static IEnumerable<PropertyValue> GetProperties(
    this ITypeBase type,
    StoreObjectIdentifier storeObjectIdentifier,
    object? row
  )
  {
    foreach (var property in type.GetProperties())
    {
      yield return new PropertyValue(
        property,
        property.GetColumnName(storeObjectIdentifier)
        ?? throw new InvalidOperationException(
          $"No column name found for {property.Name} in {type.Name}."),
        property.PropertyInfo?.GetValue(row)
        ?? property.FieldInfo?.GetValue(row));
    }

    foreach (var complexProperty in type.GetComplexProperties())
    {
      var complexRow = complexProperty.PropertyInfo?.GetValue(row)
        ?? complexProperty.FieldInfo?.GetValue(row);
      foreach (var property in GetProperties(
        complexProperty.ComplexType,
        storeObjectIdentifier,
        complexRow))
      {
        yield return property;
      }
    }
  }

  private sealed record PropertyValue(
    IProperty Property,
    string Column,
    object? Value
  );
}
