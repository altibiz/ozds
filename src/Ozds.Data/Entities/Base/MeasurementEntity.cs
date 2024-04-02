using Microsoft.EntityFrameworkCore;
using Ozds.Data.Attributes;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class MeasurementEntity : ReadonlyEntity
{
  public DateTimeOffset Timestamp { get; set; }

  public string MeterId { get; set; } = default!;
}

public class MeasurementEntity<T> : MeasurementEntity
  where T : MeterEntity
{
  public virtual T Meter { get; set; } = default!;
}

public class
  MeasurementEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration
<
  MeasurementEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    if (type == typeof(MeasurementEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(type);

    builder.HasKey(
      nameof(MeasurementEntity.Timestamp),
      nameof(MeasurementEntity.MeterId)
    );

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
      .Property<DateTimeOffset>(nameof(MeasurementEntity.Timestamp))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
