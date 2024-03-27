using System.ComponentModel.DataAnnotations.Schema;
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

  [ForeignKey(nameof(MeasurementLocation))]
  [Column(TypeName = "bigint")]
  public string MeasurementLocationId { get; set; } = default!;

  public virtual LocationMeasurementLocationEntity MeasurementLocation
  {
    get;
    set;
  } = default!;

  public virtual ICollection<LocationInvoiceEntity> Invoices { get; set; } =
    default!;

  [ForeignKey(nameof(WhiteMediumCatalogue))]
  [Column(TypeName = "bigint")]
  public string WhiteMediumCatalogueId { get; set; } = default!;

  public virtual WhiteMediumCatalogueEntity WhiteMediumCatalogue { get; set; } =
    default!;

  [ForeignKey(nameof(BlueLowCatalogue))]
  [Column(TypeName = "bigint")]
  public string BlueLowCatalogueId { get; set; } = default!;

  public virtual BlueLowCatalogueEntity BlueLowCatalogue { get; set; } =
    default!;

  [ForeignKey(nameof(WhiteLowCatalogue))]
  [Column(TypeName = "bigint")]
  public string WhiteLowCatalogueId { get; set; } = default!;

  public virtual WhiteLowCatalogueEntity WhiteLowCatalogue { get; set; } =
    default!;

  [ForeignKey(nameof(RedLowCatalogue))]
  [Column(TypeName = "bigint")]
  public string RedLowCatalogueId { get; set; } = default!;

  public virtual RedLowCatalogueEntity RedLowCatalogue { get; set; } = default!;

  [ForeignKey(nameof(RegulatoryCatalogue))]
  [Column(TypeName = "bigint")]
  public string RegulatoryCatalogueId { get; set; } = default!;

  public virtual RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } =
    default!;
}

public class LocationEntityTypeConfiguration : EntityTypeConfiguration<LocationEntity>
{
  public override void Configure(EntityTypeBuilder<LocationEntity> builder)
  {
    builder
      .HasMany(e => e.Representatives)
      .WithMany(e => e.Locations);
  }
}
