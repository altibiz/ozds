using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Data;

public partial class OzdsDbContext : DbContext
{
  public DbSet<RepresentativeEntity> Representatives { get; set; } = default!;

  public DbSet<LocationEntity> Locations { get; set; } = default!;

  public DbSet<LocationInvoiceEntity> LocationInvoices { get; set; } = default!;

  public DbSet<CatalogueEntity> Catalogues { get; set; } = default!;

  public DbSet<NetworkUserEntity> NetworkUsers { get; set; } = default!;

  public DbSet<NetworkUserInvoiceEntity> NetworkUserInvoices { get; set; } =
    default!;

  public DbSet<MeasurementLocationEntity> MeasurementLocations { get; set; } =
    default!;

  public DbSet<MessengerEntity> Messengers { get; set; } = default!;

  public DbSet<MeterEntity> Meters { get; set; } = default!;

  public DbSet<MeasurementValidatorEntity> MeasurementValidators { get; set; } =
    default!;

  public DbSet<AbbB2xMeasurementEntity> AbbB2xMeasurements { get; set; } =
    default!;

  public DbSet<AbbB2xAggregateEntity> AbbB2xAggregates { get; set; } = default!;

  public DbSet<SchneideriEM3xxxMeasurementEntity> SchneideriEM3xxxMeasurements
  {
    get;
    set;
  } =
    default!;

  public DbSet<SchneideriEM3xxxAggregateEntity> SchneideriEM3xxxAggregates
  {
    get;
    set;
  } =
    default!;

  public DbSet<EventEntity> Events { get; set; } = default!;
}
