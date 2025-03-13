using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Complex;

public class CumulativeAggregateMeasureEntity : AggregateMeasureEntity,
  ICumulativeMeasureEntity
{
  public long Min { get; set; } = default!;

  public long Max { get; set; } = default!;
}

public static class CumulativeAggregateMeasureEntityExtensions
{
  public static void CumulativeAggregateMeasure(
    this ComplexPropertyBuilder builder,
    string name,
    string unit
  )
  {
    builder.AggregateMeasure(name, unit);

    builder
      .CumulativeMeasurementMeasure(
        nameof(InstantaneousAggregateMeasureEntity.Min),
        $"{name}_min_{unit}"
      );

    builder
      .CumulativeMeasurementMeasure(
        nameof(InstantaneousAggregateMeasureEntity.Max),
        $"{name}_max_{unit}"
      );
  }
}
