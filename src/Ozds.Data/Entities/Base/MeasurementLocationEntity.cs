using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeasurementLocationEntity : AuditableEntity
{
  public string MeterId { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;
}

public class MeasurementLocationEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<MeasurementLocationEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("measurement_locations")
      .HasDiscriminator<string>("kind");

    builder
      .HasOne(nameof(MeasurementLocationEntity.Meter))
      .WithOne(nameof(MeterEntity<MeasurementEntity, AggregateEntity, MeasurementValidatorEntity>.MeasurementLocation))
      .HasForeignKey(nameof(MeasurementLocationEntity), nameof(MeasurementLocationEntity.MeterId));
  }
}
