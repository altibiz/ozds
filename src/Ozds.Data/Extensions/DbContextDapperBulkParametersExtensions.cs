using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Ozds.Data.Extensions;

public static class DbContextDapperBulkParametersExtensions
{
  public static DynamicParameters? CreateBulkParameters(
    this DbContext context,
    IEnumerable<object> measurements
  )
  {
    var parameters = new DynamicParameters();
    foreach (var (row, index) in measurements.Select((x, i) => (x, i)))
    {
      var entityType = context.Model.FindEntityType(row.GetType())
        ?? throw new InvalidOperationException(
          $"No entity type found for {row.GetType()}.");
      foreach (var property in entityType.GetProperties())
      {
        var name = $"@p{index}_{property.GetColumnName()}";
        var value = property.PropertyInfo?.GetValue(row)
          ?? property.FieldInfo?.GetValue(row);

        if (value is { } enumValue && property.ClrType.IsEnum)
        {
          value = enumValue.ToString()?.ToSnakeCase();
        }

        if (value is { })
        {
          parameters.Add(name, value);
        }
        else
        {
          parameters.Add(name, DBNull.Value);
        }
      }
    }

    return parameters;
  }
}
