using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Complex;

public class CumulativeAggregateMeasureEntity : IAggregateMeasureEntity
{
  public float Min { get; set; } = default!;

  public float Max { get; set; } = default!;
}

public static class CumulativeAggregateMeasureEntityExtensions
{
  public static void CumulativeAggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    builder
      .Property(nameof(CumulativeAggregateMeasureEntity.Min))
      .HasColumnName($"{name}_min_{unit}");

    builder
      .Property(nameof(CumulativeAggregateMeasureEntity.Max))
      .HasColumnName($"{name}_max_{unit}");
  }
}
