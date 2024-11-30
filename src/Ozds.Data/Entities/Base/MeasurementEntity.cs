using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class MeasurementEntity : IMeasurementEntity
{
  protected long _measurementLocationId;

  public DateTimeOffset Timestamp { get; set; }

  public string MeterId { get; set; } = default!;

  public virtual string MeasurementLocationId
  {
    get => _measurementLocationId.ToString();
    set => _measurementLocationId = long.Parse(value);
  }

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; }
    = default!;
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
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    if (entity == typeof(MeasurementEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(entity);

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

    builder.Ignore(nameof(MeasurementEntity.MeasurementLocationId));
    builder
      .HasOne(nameof(MeasurementEntity.MeasurementLocation))
      .WithMany()
      .HasForeignKey("_measurementLocationId");

    builder
      .Property<DateTimeOffset>(nameof(MeasurementEntity.Timestamp))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
