using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;

// TODO: make all applications work with UseTimescale

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
    dbContextOptionsBuilder
      .UseLazyLoadingProxies()
      .UseSnakeCaseNamingConvention();

  protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyPostgresqlEnums()
      .ApplyTimescaleHypertables()
      .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
