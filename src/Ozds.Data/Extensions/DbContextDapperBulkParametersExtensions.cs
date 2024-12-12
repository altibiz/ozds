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
    foreach (var (i, measurement) in measurements.Select((i, x) => (i, x)))
    {
      var entityType = context.Model.FindEntityType(measurement.GetType())
        ?? throw new InvalidOperationException(
          $"No entity type found for {measurement.GetType()}.");
      foreach (var property in entityType.GetProperties())
      {
        var name = $"@{i}-{property.GetColumnName()}";
        var value = property.PropertyInfo?.GetValue(measurement)
          ?? property.FieldInfo?.GetValue(measurement);
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
