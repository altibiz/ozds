using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Data.Context;

public partial class DataDbContext : DbContext
{
  public DbSet<RepresentativeEntity> Representatives { get; set; } = default!;

  public DbSet<LocationEntity> Locations { get; set; } = default!;

  public DbSet<LocationRepresentativeEntity> LocationRepresentatives
  {
    get;
    set;
  } = default!;

  public DbSet<NetworkUserCatalogueEntity> NetworkUserCatalogues { get; set; } =
    default!;

  public DbSet<RegulatoryCatalogueEntity> RegulatoryCatalogues { get; set; } =
    default!;

  public DbSet<NetworkUserEntity> NetworkUsers { get; set; } = default!;

  public DbSet<NetworkUserRepresentativeEntity> NetworkUserRepresentatives
  {
    get;
    set;
  } = default!;

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

  public DbSet<NetworkUserInvoiceEntity> NetworkUserInvoices { get; set; } =
    default!;

  public DbSet<NetworkUserCalculationEntity> NetworkUserCalculations
  {
    get;
    set;
  } = default!;

  public DbSet<EventEntity> Events { get; set; } = default!;

  public DbSet<NotificationEntity> Notifications { get; set; } = default!;

  public DbSet<NotificationRecipientEntity>
    NotificationRecipients { get; set; } = default!;
}
