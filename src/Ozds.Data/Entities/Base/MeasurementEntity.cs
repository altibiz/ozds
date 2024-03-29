using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class MeasurementEntity : ReadonlyEntity
{
  public DateTimeOffset Timestamp { get; set; }
}

public class MeasurementEntity<T> : MeasurementEntity
  where T : MeterEntity
{
  public string MeterId { get; set; } = default!;

  public virtual T Meter { get; set; } = default!;
}

public class
  MeasurementEntityTypeConfiguration : EntityTypeHierarchyConfiguration<
  MeasurementEntity>
{
  public override void ConfigureConcrete<T>(EntityTypeBuilder<T> builder)
  {
    builder.HasKey(
      nameof(MeasurementEntity.Timestamp),
      nameof(MeasurementEntity<MeterEntity>.MeterId)
    );

    builder.UseTpcMappingStrategy();

    builder.HasTimescaleHypertable(
      nameof(MeasurementEntity.Timestamp),
      nameof(MeasurementEntity<MeterEntity>.MeterId),
      "number_partitions => 2"
    );

    builder
      .HasOne(nameof(MeasurementEntity<MeterEntity>.Meter))
      .WithMany()
      .HasForeignKey(nameof(MeasurementEntity<MeterEntity>.MeterId));

    builder
      .Property(x => x.Timestamp)
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
