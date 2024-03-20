using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Ozds.Data.Attributes;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  public OzdsDbContext(DbContextOptions<OzdsDbContext> options) : base(options)
  {
  }

  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
    dbContextOptionsBuilder
      .ReplaceService<IMigrationsSqlGenerator, TimescaleMigrationSqlGenerator>()
      .UseSnakeCaseNamingConvention();

  protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyPostgresqlEnums()
      .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
