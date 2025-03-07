using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class AggregateEntity : IAggregateEntity
{
  protected long _measurementLocationId;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; }
    = default!;

  public DateTimeOffset Timestamp { get; set; }

  public long Count { get; set; }

  public long QuarterHourCount { get; set; }

  public IntervalEntity Interval { get; set; }

  public string MeterId { get; set; } = default!;

  public virtual string MeasurementLocationId
  {
    get { return _measurementLocationId.ToString(); }
    set { _measurementLocationId = long.Parse(value); }
  }
}

public class AggregateEntity<T> : AggregateEntity
  where T : MeterEntity
{
  public virtual T Meter { get; set; } = default!;
}

public class
  AggregateEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  AggregateEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.HasKey(
      nameof(AggregateEntity.Timestamp),
      nameof(AggregateEntity.Interval),
      nameof(AggregateEntity.MeterId),
      "_measurementLocationId"
    );

    builder
      .HasIndex(
        nameof(AggregateEntity.Timestamp),
        nameof(AggregateEntity.Interval),
        nameof(AggregateEntity.MeterId)
      )
      .IsUnique();

    builder.HasIndex(
      nameof(AggregateEntity.Timestamp),
      nameof(AggregateEntity.Interval),
      "_measurementLocationId"
    );

    builder.HasTimescaleHypertable(
      nameof(AggregateEntity.Timestamp),
      nameof(AggregateEntity<MeterEntity>.MeterId),
      "number_partitions => 2"
    );

    builder
      .HasOne(nameof(AggregateEntity<MeterEntity>.Meter))
      .WithMany()
      .HasForeignKey(nameof(AggregateEntity<MeterEntity>.MeterId));

    builder.Ignore(nameof(AggregateEntity.MeasurementLocationId));
    builder
      .Property("_measurementLocationId")
      .HasColumnName("measurement_location_id");
    builder
      .HasOne(nameof(AggregateEntity.MeasurementLocation))
      .WithMany()
      .HasForeignKey("_measurementLocationId");

    builder
      .Property<DateTimeOffset>(nameof(AggregateEntity.Timestamp))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );
  }
}
