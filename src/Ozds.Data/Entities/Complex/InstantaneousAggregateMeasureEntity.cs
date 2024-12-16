using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Complex;

public class InstantaneousAggregateMeasureEntity : IAggregateMeasureEntity
{
  public float Avg { get; set; } = default!;

  public float Min { get; set; } = default!;

  public float Max { get; set; } = default!;

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
    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.Avg))
      .HasColumnName($"{name}_avg_{unit}");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.Min))
      .HasColumnName($"{name}_min_{unit}");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.Max))
      .HasColumnName($"{name}_max_{unit}");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.MinTimestamp))
      .HasColumnName($"{name}_min_timestamp");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.MaxTimestamp))
      .HasColumnName($"{name}_max_timestamp");
  }
}
