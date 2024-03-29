using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationEntity : AuditableEntity
{
  public virtual ICollection<RepresentativeEntity>
    Representatives
  { get; set; } = default!;

  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } =
    default!;

  public virtual ICollection<MessengerEntity> Messengers { get; set; } =
    default!;

  public virtual ICollection<LocationMeasurementLocationEntity>
    MeasurementLocations
  { get; set; } = default!;

  public virtual ICollection<LocationInvoiceEntity> Invoices { get; set; } =
    default!;

  private long _whiteMediumCatalogueId = default!;

  public string WhiteMediumCatalogueId
  {
    get => _whiteMediumCatalogueId.ToString();
    init => _whiteMediumCatalogueId = long.Parse(value);
  }

  public virtual WhiteMediumCatalogueEntity WhiteMediumCatalogue { get; set; } =
    default!;

  private long _blueLowCatalogueId = default!;

  public string BlueLowCatalogueId
  {
    get => _blueLowCatalogueId.ToString();
    init => _blueLowCatalogueId = long.Parse(value);
  }

  public virtual BlueLowCatalogueEntity BlueLowCatalogue { get; set; } =
    default!;

  private long _whiteLowCatalogueId = default!;

  public string WhiteLowCatalogueId
  {
    get => _whiteLowCatalogueId.ToString();
    init => _whiteLowCatalogueId = long.Parse(value);
  }

  public virtual WhiteLowCatalogueEntity WhiteLowCatalogue { get; set; } =
    default!;

  private long _redLowCatalogueId = default!;

  public string RedLowCatalogueId
  {
    get => _redLowCatalogueId.ToString();
    init => _redLowCatalogueId = long.Parse(value);
  }

  public virtual RedLowCatalogueEntity RedLowCatalogue { get; set; } = default!;

  private long _regulatoryCatalogueId = default!;

  public string RegulatoryCatalogueId
  {
    get => _regulatoryCatalogueId.ToString();
    init => _regulatoryCatalogueId = long.Parse(value);
  }

  public virtual RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } =
    default!;
}

public class
  LocationEntityTypeConfiguration : EntityTypeConfiguration<LocationEntity>
{
  public override void Configure(EntityTypeBuilder<LocationEntity> builder)
  {
    builder
      .HasMany(nameof(LocationEntity.Representatives))
      .WithMany(nameof(RepresentativeEntity.Locations));

    builder
      .HasMany(nameof(LocationEntity.NetworkUsers))
      .WithOne(nameof(NetworkUserEntity.Location));

    builder
      .HasMany(nameof(LocationEntity.Messengers))
      .WithOne(nameof(MessengerEntity.Location));

    builder
      .HasMany(nameof(LocationEntity.MeasurementLocations))
      .WithOne(nameof(LocationMeasurementLocationEntity.Location));

    builder
      .HasMany(nameof(LocationEntity.Invoices))
      .WithOne(nameof(LocationInvoiceEntity.Location));

    builder
      .HasOne(nameof(LocationEntity.WhiteMediumCatalogue))
      .WithOne(nameof(WhiteMediumCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), "_whiteMediumCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.BlueLowCatalogue))
      .WithOne(nameof(BlueLowCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), "_blueLowCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.WhiteLowCatalogue))
      .WithOne(nameof(WhiteLowCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), "_whiteLowCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.RedLowCatalogue))
      .WithOne(nameof(RedLowCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), "_redLowCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.RegulatoryCatalogue))
      .WithOne(nameof(RegulatoryCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), "_regulatoryCatalogueId");

    builder.Ignore(nameof(LocationEntity.WhiteMediumCatalogueId));
    builder
      .Property("_whiteMediumCatalogueId")
      .HasColumnName("white_medium_catalogue_id");

    builder.Ignore(nameof(LocationEntity.BlueLowCatalogueId));
    builder
      .Property("_blueLowCatalogueId")
      .HasColumnName("blue_low_catalogue_id");

    builder.Ignore(nameof(LocationEntity.WhiteLowCatalogueId));
    builder
      .Property("_whiteLowCatalogueId")
      .HasColumnName("white_low_catalogue_id");

    builder.Ignore(nameof(LocationEntity.RedLowCatalogueId));
    builder
      .Property("_redLowCatalogueId")
      .HasColumnName("red_low_catalogue_id");

    builder.Ignore(nameof(LocationEntity.RegulatoryCatalogueId));
    builder
      .Property("_regulatoryCatalogueId")
      .HasColumnName("regulatory_catalogue_id");
  }
}
