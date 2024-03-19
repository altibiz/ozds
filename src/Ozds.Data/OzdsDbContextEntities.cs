using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  public DbSet<NetworkUserEntity> NetworkUsers { get; set; } = default!;
  public DbSet<LocationEntity> Locations { get; set; } = default!;
  public DbSet<RepresentativeEntity> Representatives { get; set; } = default!;

  public DbSet<AbbB2xMeasurementEntity> AbbB2xMeasurements { get; set; } = default!;

  public DbSet<SchneideriEM3xxxMeasurementEntity> SchneideriEM3xxxMeasurements { get; set; } =
    default!;
}
