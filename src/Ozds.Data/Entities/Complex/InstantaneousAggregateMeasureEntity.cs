using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public class InstantaneousAggregateMeasureEntity : AggregateMeasureEntity
{
  public float Avg { get; set; } = default!;

  public DateTimeOffset MinTimestamp { get; set; } = default!;

  public DateTimeOffset MaxTimestamp { get; set; } = default!;
}

public static class InstantaneousAggregateMeasureEntityExtensions
{
  public static void InstantaneousAggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    builder.AggregateMeasure(name, unit);

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.Avg))
      .HasColumnName($"{name}_avg_{unit}");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.MinTimestamp))
      .HasColumnName($"{name}_min_timestamp");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.MaxTimestamp))
      .HasColumnName($"{name}_max_timestamp");
  }
}
