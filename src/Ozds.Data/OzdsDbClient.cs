using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Entities;

namespace Ozds.Data;

public class OzdsDbClient(OzdsDbContext context)
{
  private readonly OzdsDbContext _context = context;

  public async Task MigrateAsync()
  {
    await _context.Database.MigrateAsync();

    var entityTypes = _context.Model.GetEntityTypes();
    foreach (var entityType in entityTypes)
    {
      var tableName = entityType.GetTableName();
      if (tableName == null)
      {
        continue;
      }

      foreach (var property in entityType.GetProperties())
      {
        if (
          property.PropertyInfo?.GetCustomAttribute(
            typeof(HypertableColumnAttribute)
          )
          is null
        )
        {
          continue;
        }

        var columnName = property.GetColumnName();
        try
        {
#pragma warning disable EF1002
          await _context.Database.ExecuteSqlRawAsync(
            $"SELECT create_hypertable('\"{tableName}\"', '{columnName}');"
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
