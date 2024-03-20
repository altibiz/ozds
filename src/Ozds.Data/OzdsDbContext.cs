using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Ozds.Data.Attributes;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
    dbContextOptionsBuilder
      .UseNpgsql(this.GetService<IConfiguration>().GetConnectionString("Ozds"))
      .ReplaceService<IMigrationsSqlGenerator, TimescaleMigrationSqlGenerator>()
      .UseSnakeCaseNamingConvention();

  protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyPostgresqlEnums()
      .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
