using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Complex;

public class DerivedAggregateMeasureEntity
  : AggregateMeasureEntity, IDerivedMeasureEntity
{
  public DateTimeOffset MinTimestamp { get; set; } = default!;

  public DateTimeOffset MaxTimestamp { get; set; } = default!;
  public long Min { get; set; } = default!;

  public long Max { get; set; } = default!;

  public double Avg { get; set; } = default!;
}

public static class DerivedAggregateMeasureEntityExtensions
{
  public static void DerivedAggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    builder.AggregateMeasure(name, unit);

    builder
      .Property(nameof(DerivedAggregateMeasureEntity.Min))
      .HasColumnName($"{name}_min_{unit}")
      .HasColumnType("bigint");

    builder
      .Property(nameof(DerivedAggregateMeasureEntity.Max))
      .HasColumnName($"{name}_max_{unit}")
      .HasColumnType("bigint");

    builder
      .Property(nameof(DerivedAggregateMeasureEntity.Avg))
      .HasColumnName($"{name}_avg_{unit}")
      .HasColumnType("double precision");

    builder
      .Property(nameof(DerivedAggregateMeasureEntity.MinTimestamp))
      .HasColumnName($"{name}_min_timestamp");

    builder
      .Property(nameof(DerivedAggregateMeasureEntity.MaxTimestamp))
      .HasColumnName($"{name}_max_timestamp");
  }
}
