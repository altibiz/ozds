using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Context;

public partial class DataDbContext(
  DbContextOptions<DataDbContext> options)
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
      .ApplyModelConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    // TODO: create migration
#pragma warning disable S125 // Sections of code should not be commented out
    // foreach (var relationship in modelBuilder.Model
    //   .GetEntityTypes()
    //   .SelectMany(e => e.GetForeignKeys()))
    // Sections of code should not be commented out
    // {
    //   relationship.DeleteBehavior = DeleteBehavior.Restrict;
    // }
#pragma warning restore S125 // Sections of code should not be commented out

    base.OnModelCreating(modelBuilder);
  }
}
