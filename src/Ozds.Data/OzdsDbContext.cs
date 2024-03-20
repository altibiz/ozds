using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Ozds.Data.Attributes;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
    dbContextOptionsBuilder
      .UseNpgsql("Host=localhost;Database=ozds;Username=ozds;Password=ozds")
      .ReplaceService<IMigrationsSqlGenerator, TimescaleMigrationSqlGenerator>()
      .UseSnakeCaseNamingConvention();

  protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyPostgresqlEnums()
      .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
