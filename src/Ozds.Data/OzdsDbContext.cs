using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

// TODO: make all applications work with UseTimescale

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
    dbContextOptionsBuilder
      .UseTimescale("Host=localhost;Database=ozds;Username=ozds;Password=ozds")
      .UseLazyLoadingProxies()
      .UseSnakeCaseNamingConvention();

  protected override void OnModelCreating(ModelBuilder modelBuilder) =>
    modelBuilder
      .HasPostgresExtension("timescaledb")
      .ApplyPostgresqlEnums()
      .ApplyTimescaleHypertables()
      .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

  public override int SaveChanges()
  {
    ThrowOnReadonlyMutation();
    SoftenDeletion();
    return base.SaveChanges();
  }

  public override int SaveChanges(bool acceptAllChangesOnSuccess)
  {
    ThrowOnReadonlyMutation();
    SoftenDeletion();
    return base.SaveChanges(acceptAllChangesOnSuccess);
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    ThrowOnReadonlyMutation();
    SoftenDeletion();
    return base.SaveChangesAsync(cancellationToken);
  }

  public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
  {
    ThrowOnReadonlyMutation();
    SoftenDeletion();
    return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
  }

  private void SoftenDeletion()
  {
    foreach (var entry in ChangeTracker.Entries<SoftDeletableEntity>())
    {
      if (entry.State is EntityState.Deleted)
      {
        entry.State = EntityState.Modified;
        entry.Entity.IsDeleted = true;
      }
    }
  }

  private void ThrowOnReadonlyMutation()
  {
    foreach (var entry in ChangeTracker.Entries<ReadonlyEntity>())
    {
      if (entry.State is EntityState.Modified or EntityState.Deleted)
      {
        throw new InvalidOperationException("Cannot modify readonly entities.");
      }
    }
  }
}
