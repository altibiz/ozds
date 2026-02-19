using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Procedures.Builders;

namespace Ozds.Data.Entities.Complex;

public class InstantaneousAggregateMeasureEntity
  : AggregateMeasureEntity,
    IInstantaneousMeasureEntity
{
  public DateTimeOffset MinTimestamp { get; set; } = default!;

  public DateTimeOffset MaxTimestamp { get; set; } = default!;
  public float Min { get; set; } = default!;

  public float Max { get; set; } = default!;

  public float Avg { get; set; } = default!;
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

    builder.InstantaneousMeasurementMeasure(
      nameof(InstantaneousAggregateMeasureEntity.Min),
      $"{name}_min_{unit}"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(InstantaneousAggregateMeasureEntity.Max),
      $"{name}_max_{unit}"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(InstantaneousAggregateMeasureEntity.Avg),
      $"{name}_avg_{unit}"
    );

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.MinTimestamp))
      .HasColumnName($"{name}_min_timestamp");

    builder
      .Property(nameof(InstantaneousAggregateMeasureEntity.MaxTimestamp))
      .HasColumnName($"{name}_max_timestamp");
  }

  public static MeasurementProcedureBuilder<T> InstantaneousAggregateMeasure<T>(
    this MeasurementProcedureBuilder<T> builder,
    Expression<Func<T, InstantaneousAggregateMeasureEntity>> value
  )
  {
    return builder
      .UpsertAverage(value.Suffix(x => x.Avg))
      .UpsertMin(value.Suffix(x => x.Min))
      .UpsertMinTimestamp(
        value.Suffix(x => x.Min),
        value.Suffix(x => x.MinTimestamp)
      )
      .UpsertMax(value.Suffix(x => x.Max))
      .UpsertMaxTimestamp(
        value.Suffix(x => x.Max),
        value.Suffix(x => x.MaxTimestamp)
      );
  }
}
