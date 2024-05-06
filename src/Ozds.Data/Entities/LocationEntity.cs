using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationEntity : AuditableEntity
{
  private long _blueLowNetworkUserCatalogueId;

  private long _redLowNetworkUserCatalogueId;

  private long _regulatoryNetworkUserCatalogueId;

  private long _whiteLowNetworkUserCatalogueId;

  private long _whiteMediumNetworkUserCatalogueId;

  public virtual ICollection<RepresentativeEntity>
    Representatives { get; set; } = default!;

  public virtual ICollection<NetworkUserEntity> NetworkUsers { get; set; } =
    default!;

  public virtual ICollection<MessengerEntity> Messengers { get; set; } =
    default!;

  public virtual ICollection<LocationMeasurementLocationEntity>
    MeasurementLocations { get; set; } = default!;

  public virtual ICollection<LocationInvoiceEntity> Invoices { get; set; } =
    default!;

  public string WhiteMediumNetworkUserCatalogueId
  {
    get { return _whiteMediumNetworkUserCatalogueId.ToString(); }
    init { _whiteMediumNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual WhiteMediumNetworkUserCatalogueEntity
    WhiteMediumNetworkUserCatalogue { get; set; } =
    default!;

  public string BlueLowNetworkUserCatalogueId
  {
    get { return _blueLowNetworkUserCatalogueId.ToString(); }
    init { _blueLowNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual BlueLowNetworkUserCatalogueEntity BlueLowNetworkUserCatalogue
  {
    get;
    set;
  } =
    default!;

  public string WhiteLowNetworkUserCatalogueId
  {
    get { return _whiteLowNetworkUserCatalogueId.ToString(); }
    init { _whiteLowNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual WhiteLowNetworkUserCatalogueEntity WhiteLowNetworkUserCatalogue
  {
    get;
    set;
  } =
    default!;

  public string RedLowNetworkUserCatalogueId
  {
    get { return _redLowNetworkUserCatalogueId.ToString(); }
    init { _redLowNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual RedLowNetworkUserCatalogueEntity RedLowNetworkUserCatalogue
  {
    get;
    set;
  } = default!;

  public string RegulatoryCatalogueId
  {
    get { return _regulatoryNetworkUserCatalogueId.ToString(); }
    init { _regulatoryNetworkUserCatalogueId = long.Parse(value); }
  }

  public virtual RegulatoryCatalogueEntity RegulatoryCatalogue { get; set; } =
    default!;

  public LegalPersonEntity LegalPerson { get; set; } = default!;
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
      .HasOne(nameof(LocationEntity.WhiteMediumNetworkUserCatalogue))
      .WithMany(nameof(WhiteMediumNetworkUserCatalogueEntity.Locations))
      .HasForeignKey("_whiteMediumNetworkUserCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.BlueLowNetworkUserCatalogue))
      .WithMany(nameof(BlueLowNetworkUserCatalogueEntity.Locations))
      .HasForeignKey("_blueLowNetworkUserCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.WhiteLowNetworkUserCatalogue))
      .WithMany(nameof(WhiteLowNetworkUserCatalogueEntity.Locations))
      .HasForeignKey("_whiteLowNetworkUserCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.RedLowNetworkUserCatalogue))
      .WithMany(nameof(RedLowNetworkUserCatalogueEntity.Locations))
      .HasForeignKey("_redLowNetworkUserCatalogueId");

    builder
      .HasOne(nameof(LocationEntity.RegulatoryCatalogue))
      .WithMany(nameof(RegulatoryCatalogueEntity.Locations))
      .HasForeignKey("_regulatoryNetworkUserCatalogueId");

    builder.Ignore(nameof(LocationEntity.WhiteMediumNetworkUserCatalogueId));
    builder
      .Property("_whiteMediumNetworkUserCatalogueId")
      .HasColumnName("white_medium_catalogue_id");

    builder.Ignore(nameof(LocationEntity.BlueLowNetworkUserCatalogueId));
    builder
      .Property("_blueLowNetworkUserCatalogueId")
      .HasColumnName("blue_low_catalogue_id");

    builder.Ignore(nameof(LocationEntity.WhiteLowNetworkUserCatalogueId));
    builder
      .Property("_whiteLowNetworkUserCatalogueId")
      .HasColumnName("white_low_catalogue_id");

    builder.Ignore(nameof(LocationEntity.RedLowNetworkUserCatalogueId));
    builder
      .Property("_redLowNetworkUserCatalogueId")
      .HasColumnName("red_low_catalogue_id");

    builder.Ignore(nameof(LocationEntity.RegulatoryCatalogueId));
    builder
      .Property("_regulatoryNetworkUserCatalogueId")
      .HasColumnName("regulatory_catalogue_id");

    builder.ComplexProperty(nameof(LocationEntity.LegalPerson));
  }
}
