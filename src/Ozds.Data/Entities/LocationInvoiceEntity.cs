using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationInvoiceEntity : InvoiceEntity
{
  public string LocationId { get; set; } = default!;
  public virtual LocationEntity Location { get; set; } = default!;
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
      .HasForeignKey(nameof(LocationInvoiceEntity.LocationId));

    builder
      .Property(nameof(LocationInvoiceEntity.LocationId))
      .HasColumnType("bigint")
      .HasConversion<long>();
  }
}
