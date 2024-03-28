using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ozds.Data.Timescale;

namespace Ozds.Data.Extensions;

public static class DbContextOptionsBuilderExtensions
{
  public static DbContextOptionsBuilder UseTimescale(
    this DbContextOptionsBuilder builder,
    string connectionString
  )
  {
    return builder
      .UseNpgsql(connectionString)
      .ReplaceService<IMigrationsSqlGenerator, TimescaleMigrationSqlGenerator>()
      .ReplaceService<IRelationalAnnotationProvider, TimescaleAnnotationProvider>();
  }
}
