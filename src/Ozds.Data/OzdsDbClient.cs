using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Entities;

namespace Ozds.Data;

public partial class OzdsDbClient(OzdsDbContext context)
{
  public async Task MigrateAsync()
  {
    await context.Database.MigrateAsync();

    var entityTypes = context.Model.GetEntityTypes();
    foreach (var entityType in entityTypes)
    {
      var tableName = entityType.GetTableName();
      if (tableName == null)
      {
        continue;
      }

      var hypertableProperties = entityType
        .GetProperties()
        .Where(property => property.PropertyInfo
          ?.GetCustomAttribute(typeof(HypertableColumnAttribute))
          is not null
        )
        .ToList();

      if (hypertableProperties.Count == 0)
      {
        continue;
      }

      var firstProperty = hypertableProperties[0];
      var restProperties = hypertableProperties[1..];

      try
      {
#pragma warning disable EF1002
        _ = await context.Database.ExecuteSqlRawAsync(
          $"SELECT create_hypertable('\"{tableName}\"', '{firstProperty.GetColumnName()}');"
        );
#pragma warning restore EF1002
      }
      catch (PostgresException exception) when (exception.SqlState == "TS110")
      {
        // NOTE: already a hypertable
      }

      foreach (var property in restProperties)
      {
        try
        {
#pragma warning disable EF1002
          _ = await context.Database.ExecuteSqlRawAsync(
            $"SELECT add_dimension('\"{tableName}\"', '{property.GetColumnName()}');"
          );
#pragma warning restore EF1002
        }
        catch (PostgresException exception) when (exception.SqlState == "TS110")
        {
          // NOTE: already a hypertable
        }
      }
    }
  }
}
