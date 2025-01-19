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
    var builder = modelBuilder.Entity(entity);

    builder.HasKey(
      nameof(MeasurementEntity.Timestamp),
      nameof(MeasurementEntity.MeterId),
      "_measurementLocationId"
    );

    builder
      .HasIndex(
        nameof(AggregateEntity.Timestamp),
        nameof(AggregateEntity.MeterId)
      )
      .IsUnique();

    builder.HasIndex(
      nameof(AggregateEntity.Timestamp),
      "_measurementLocationId"
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
      .Property("_measurementLocationId")
      .HasColumnName("measurement_location_id");
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
