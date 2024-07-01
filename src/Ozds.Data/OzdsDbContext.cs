using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

// TODO: remove all the applications in favor of fluent API

namespace Ozds.Data;

public partial class OzdsDbContext(DbContextOptions<OzdsDbContext> options)
  : DbContext(options)
{
  protected override void OnConfiguring(
    DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder
      .UseLazyLoadingProxies()
      .UseSnakeCaseNamingConvention();

    base.OnConfiguring(optionsBuilder);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyModelConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
      .ApplyPostgresqlEnumAttributes()
      .ApplyTimescaleHypertableAttributes();

    base.OnModelCreating(modelBuilder);
  }
}
