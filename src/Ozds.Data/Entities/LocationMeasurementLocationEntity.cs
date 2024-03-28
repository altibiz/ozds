using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationMeasurementLocationEntity : MeasurementLocationEntity
{
  public string LocationId { get; set; } = default!;
  public virtual LocationEntity Location { get; set; } = default!;
}

public class LocationMeasurementLocationEntityTypeConfiguration : EntityTypeConfiguration<LocationMeasurementLocationEntity>
{
  public override void Configure(EntityTypeBuilder<LocationMeasurementLocationEntity> builder)
  {
    builder
      .HasOne(nameof(LocationMeasurementLocationEntity.Location))
      .WithOne(nameof(LocationEntity.MeasurementLocation))
      .HasForeignKey(nameof(LocationEntity.MeasurementLocationId));

    builder
      .Property(nameof(LocationMeasurementLocationEntity.LocationId))
      .HasColumnType("bigint")
      .HasConversion<long>();
  }
}
