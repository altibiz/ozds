using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class NetworkUserEntity : AuditableEntity
{
  private long _locationId;

  public virtual ICollection<RepresentativeEntity>
    Representatives { get; set; } = default!;

  public virtual ICollection<NetworkUserRepresentativeEntity>
    NetworkUserRepresentatives { get; set; } = default!;

  public virtual string LocationId
  {
    get { return _locationId.ToString(); }
    set { _locationId = long.Parse(value); }
  }

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<NetworkUserMeasurementLocationEntity>
    NetworkUserMeasurementLocations { get; set; } = default!;

  public virtual ICollection<NetworkUserInvoiceEntity> Invoices { get; set; } =
    default!;

  public LegalPersonEntity LegalPerson { get; set; } = default!;

  public string AltiBizSubProjectCode { get; set; } = default!;
}

public class
  NetworkUserEntityTypeConfiguration : EntityTypeConfiguration<
  NetworkUserEntity>
{
  public override void Configure(EntityTypeBuilder<NetworkUserEntity> builder)
  {
    builder
      .HasOne(nameof(NetworkUserEntity.Location))
      .WithMany(nameof(LocationEntity.NetworkUsers))
      .HasForeignKey("_locationId");

    builder
      .HasMany(nameof(NetworkUserEntity.NetworkUserMeasurementLocations))
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
