using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

// TODO: make all applications work with UseTimescale

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  public OzdsDbContext(DbContextOptions<OzdsDbContext> options)
    : base(options)
  {
  }

  protected override void OnConfiguring(
    DbContextOptionsBuilder dbContextOptionsBuilder)
  {
    dbContextOptionsBuilder
      .UseLazyLoadingProxies()
      .UseSnakeCaseNamingConvention();

    base.OnConfiguring(dbContextOptionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyModelConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
      .ApplyPostgresqlEnums()
      .ApplyTimescaleHypertables()
      .UseIdentityAlwaysColumns();

    base.OnModelCreating(modelBuilder);
  }
}
