using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationInvoiceEntity : InvoiceEntity
{
  private long _locationId;

  public virtual string LocationId
  {
    get { return _locationId.ToString(); }
    init { _locationId = long.Parse(value); }
  }

  public virtual LocationEntity Location { get; set; } = default!;

  public LocationEntity ArchivedLocation { get; set; } = default!;
}

public class
  LocationInvoiceEntityTypeConfiguration : EntityTypeConfiguration<
  LocationInvoiceEntity>
{
  public override void Configure(
    EntityTypeBuilder<LocationInvoiceEntity> builder)
  {
    builder
      .HasOne(nameof(LocationInvoiceEntity.Location))
      .WithMany(nameof(LocationEntity.Invoices))
      .HasForeignKey("_locationId");

    builder.Ignore(nameof(LocationInvoiceEntity.LocationId));
    builder
      .Property("_locationId")
      .HasColumnName("location_id");

    builder
      .ArchivedProperty(nameof(LocationInvoiceEntity.ArchivedLocation));
  }
}
