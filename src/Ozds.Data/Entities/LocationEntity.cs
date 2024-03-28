using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationEntity : AuditableEntity
{
  public virtual ICollection<RepresentativeEntity> Representatives { get; set; } = default!;
  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } = default!;
  public virtual ICollection<MessengerEntity> Messengers { get; set; } = default!;
  public string MeasurementLocationId { get; set; } = default!;
  public virtual ICollection<LocationMeasurementLocationEntity> MeasurementLocations { get; set; } = default!;
  public virtual ICollection<LocationInvoiceEntity> Invoices { get; set; } = default!;
  public string WhiteMediumCatalogueId { get; set; } = default!;
  public virtual WhiteMediumCatalogueEntity WhiteMediumCatalogue { get; set; } = default!;
  public string BlueLowCatalogueId { get; set; } = default!;
  public virtual BlueLowCatalogueEntity BlueLowCatalogue { get; set; } = default!;
  public string WhiteLowCatalogueId { get; set; } = default!;
  public virtual WhiteLowCatalogueEntity WhiteLowCatalogue { get; set; } = default!;
  public string RedLowCatalogueId { get; set; } = default!;
  public virtual RedLowCatalogueEntity RedLowCatalogue { get; set; } = default!;
  public string RegulatoryCatalogueId { get; set; } = default!;
  public virtual RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } = default!;
}

public class LocationEntityTypeConfiguration : EntityTypeConfiguration<LocationEntity>
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
      .HasForeignKey(nameof(LocationEntity), nameof(LocationEntity.WhiteMediumCatalogueId));

    builder
      .HasOne(nameof(LocationEntity.BlueLowCatalogue))
      .WithOne(nameof(BlueLowCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), nameof(LocationEntity.BlueLowCatalogueId));

    builder
      .HasOne(nameof(LocationEntity.WhiteLowCatalogue))
      .WithOne(nameof(WhiteLowCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), nameof(LocationEntity.WhiteLowCatalogueId));

    builder
      .HasOne(nameof(LocationEntity.RedLowCatalogue))
      .WithOne(nameof(RedLowCatalogueEntity.Location))
      .HasForeignKey(nameof(LocationEntity), nameof(LocationEntity.RedLowCatalogueId));

    builder
      .Property(nameof(LocationEntity.MeasurementLocationId))
      .HasColumnType("bigint")
      .HasConversion<long>();

    builder
      .Property(nameof(LocationEntity.WhiteMediumCatalogueId))
      .HasColumnType("bigint")
      .HasConversion<long>();

    builder
      .Property(nameof(LocationEntity.BlueLowCatalogueId))
      .HasColumnType("bigint")
      .HasConversion<long>();

    builder
      .Property(nameof(LocationEntity.WhiteLowCatalogueId))
      .HasColumnType("bigint")
      .HasConversion<long>();

    builder
      .Property(nameof(LocationEntity.RedLowCatalogueId))
      .HasColumnType("bigint")
      .HasConversion<long>();
  }
}
