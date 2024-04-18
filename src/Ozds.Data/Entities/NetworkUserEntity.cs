using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserEntity : AuditableEntity
{
  private readonly long _locationId;

  public virtual ICollection<RepresentativeEntity>
    Representatives { get; set; } = default!;

  public virtual string LocationId
  {
    get { return _locationId.ToString(); }
    init { _locationId = long.Parse(value); }
  }

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<NetworkUserMeasurementLocationEntity>
    MeasurementLocations { get; set; } = default!;

  public virtual ICollection<NetworkUserInvoiceEntity> Invoices { get; set; } =
    default!;

  public LegalPersonEntity LegalPerson { get; set; } = default!;
}

public class
  NetworkUserEntityTypeConfiguration : EntityTypeConfiguration<
  NetworkUserEntity>
{
  public override void Configure(EntityTypeBuilder<NetworkUserEntity> builder)
  {
    builder
      .HasMany(nameof(NetworkUserEntity.Representatives))
      .WithMany(nameof(RepresentativeEntity.NetworkUsers));

    builder
      .HasOne(nameof(NetworkUserEntity.Location))
      .WithMany(nameof(LocationEntity.NetworkUsers))
      .HasForeignKey("_locationId");

    builder
      .HasMany(nameof(NetworkUserEntity.MeasurementLocations))
      .WithOne(nameof(NetworkUserMeasurementLocationEntity.NetworkUser));

    builder
      .HasMany(nameof(NetworkUserEntity.Invoices))
      .WithOne(nameof(NetworkUserInvoiceEntity.NetworkUser));

    builder.Ignore(nameof(NetworkUserEntity.LocationId));
    builder
      .Property("_locationId")
      .HasColumnName("location_id");

    builder.ComplexProperty(nameof(NetworkUserEntity.LegalPerson));
  }
}
