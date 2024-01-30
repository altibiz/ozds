using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  public DbSet<TenantEntity> Tenants { get; set; } = default!;
  public DbSet<SubnetEntity> Subnets { get; set; } = default!;
  public DbSet<RepresentativeEntity> Representatives { get; set; } = default!;
  public DbSet<AbbMeasurementEntity> AbbMeasurements { get; set; } = default!;
  public DbSet<SchneiderMeasurementEntity> SchneiderMeasurements { get; set; } = default!;
}
