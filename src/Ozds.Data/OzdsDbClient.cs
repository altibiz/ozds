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

      var timeProperty = hypertableProperties.FirstOrDefault(property =>
        property.DeclaringType.ClrType == typeof(DateTime) ||
        property.DeclaringType.ClrType == typeof(DateTimeOffset)
      );
      if (timeProperty is null)
      {
        continue;
      }

      var restProperties = hypertableProperties
        .Where(property => property != timeProperty)
        .ToList();

      var createHypertableColumnArgs = restProperties.Count is 0
        ? $"'\"{tableName}\"', '{timeProperty.GetColumnName()}'"
        : $"'\"{tableName}\"', '{timeProperty.GetColumnName()}', '{string.Join("', '", restProperties.Select(property => property.GetColumnName()))}'";

      var createHypertableChunkingArgs = restProperties.Count is 0
        ? $"chunk_time_interval => INTERVAL '1 day'"
        : $"chunk_time_interval => INTERVAL '1 day', partitioning_column => '{restProperties.First().GetColumnName()}'";

      try
      {
#pragma warning disable EF1002
        _ = await context.Database.ExecuteSqlRawAsync(
          $"SELECT create_hypertable({createHypertableColumnArgs}, {createHypertableChunkingArgs});"
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
