using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

// TODO: make all applications work with UseTimescale

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
    dbContextOptionsBuilder
      .UseTimescale("Host=localhost;Database=ozds;Username=ozds;Password=ozds")
      .UseSnakeCaseNamingConvention();

  protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyPostgresqlEnums()
      .ApplyTimescaleHypertables()
      .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
