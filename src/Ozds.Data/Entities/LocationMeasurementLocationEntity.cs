using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class LocationMeasurementLocationEntity : MeasurementLocationEntity
{
  private long _locationId = default!;
  public virtual string LocationId { get => _locationId.ToString(); init => _locationId = long.Parse(value); }
  public virtual LocationEntity Location { get; set; } = default!;
}

public class
  LocationMeasurementLocationEntityTypeConfiguration : EntityTypeConfiguration<
  LocationMeasurementLocationEntity>
{
  public override void Configure(
    EntityTypeBuilder<LocationMeasurementLocationEntity> builder)
  {
    builder
      .HasOne(nameof(LocationMeasurementLocationEntity.Location))
      .WithMany(nameof(LocationEntity.MeasurementLocations))
      .HasForeignKey("_locationId");

    builder.Ignore(nameof(LocationMeasurementLocationEntity.LocationId));
    builder
      .Property("_locationId")
      .HasColumnName("location_id");
  }
}
