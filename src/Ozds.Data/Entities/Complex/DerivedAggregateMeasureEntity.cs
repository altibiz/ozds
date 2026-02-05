using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;
using Ozds.Data.Procedures.Builders;

namespace Ozds.Data.Entities.Complex;

public class DerivedAggregateMeasureEntity
  : AggregateMeasureEntity,
    IDerivedMeasureEntity
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

  public static MeasurementProcedureBuilder<T> DerivedAggregateMeasure<T>(
    this MeasurementProcedureBuilder<T> builder,
    Expression<Func<T, DerivedAggregateMeasureEntity>> value,
    Expression<Func<T, CumulativeAggregateMeasureEntity>> cumulative
  )
  {
    return builder
      .DerivativePower(
        value.Suffix(x => x.Avg),
        cumulative.Suffix(x => x.Min),
        cumulative.Suffix(x => x.Max)
      )
      .DerivativePower(
        value.Suffix(x => x.Min),
        cumulative.Suffix(x => x.Min),
        cumulative.Suffix(x => x.Max)
      )
      .DerivativePowerTimestamp(
        value.Suffix(x => x.Min),
        value.Suffix(x => x.MinTimestamp)
      )
      .DerivativePower(
        value.Suffix(x => x.Max),
        cumulative.Suffix(x => x.Min),
        cumulative.Suffix(x => x.Max)
      )
      .DerivativePowerTimestamp(
        value.Suffix(x => x.Max),
        value.Suffix(x => x.MaxTimestamp)
      )
      .DeltaAverage(value.Suffix(x => x.Avg))
      .DeltaMin(value.Suffix(x => x.Min))
      .DeltaMinTimestamp(
        value.Suffix(x => x.Min),
        value.Suffix(x => x.MinTimestamp)
      )
      .DeltaMax(value.Suffix(x => x.Max))
      .DeltaMaxTimestamp(
        value.Suffix(x => x.Max),
        value.Suffix(x => x.MaxTimestamp)
      )
      .DeriveAverage(value.Suffix(x => x.Avg))
      .DeriveMin(value.Suffix(x => x.Min))
      .DeriveMinTimestamp(
        value.Suffix(x => x.Min),
        value.Suffix(x => x.MinTimestamp)
      )
      .DeriveMax(value.Suffix(x => x.Max))
      .DeriveMaxTimestamp(
        value.Suffix(x => x.Max),
        value.Suffix(x => x.MaxTimestamp)
      );
  }
}
