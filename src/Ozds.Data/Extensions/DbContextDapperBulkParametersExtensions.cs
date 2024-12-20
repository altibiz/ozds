using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ozds.Data.Extensions;

// TODO: handle nesting hierarchical properties

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
      var properties = entityType.GetProperties()
        .Select(p =>
          (p,
          p.GetColumnName(storeObjectIdentifier),
          p.PropertyInfo?.GetValue(row) ?? p.FieldInfo?.GetValue(row)))
        .Concat(entityType
          .GetComplexProperties()
          .SelectMany(p =>
          {
            var nested = p.PropertyInfo?.GetValue(row)
              ?? p.FieldInfo?.GetValue(row);
            return p.ComplexType
              .GetProperties()
              .Select(p =>
                (p,
                p.GetColumnName(storeObjectIdentifier),
                p.PropertyInfo?.GetValue(nested)
                  ?? p.FieldInfo?.GetValue(nested)));
          }))
        .ToList();

      foreach (var (property, columnName, value) in properties)
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

        if (updated is { })
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
}
