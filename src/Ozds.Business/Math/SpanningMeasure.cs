using System.Numerics;

namespace Ozds.Business.Math;

#pragma warning disable S3060

public record class MinMaxSpanningMeasure<T>(
  TariffMeasure<T> TrueMin,
  TariffMeasure<T> TrueMax)
  : SpanningMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class AvgSpanningMeasure<T>(
  TariffMeasure<T> TrueAvg)
  : SpanningMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class PeakSpanningMeasure<T>(
  TariffMeasure<T> TruePeak)
  : SpanningMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public record class NullSpanningMeasure<T> : SpanningMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>;

public abstract record class SpanningMeasure<T>
  where T : struct,
  IComparisonOperators<T, T, bool>,
  IAdditionOperators<T, T, T>,
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, T, T>,
  IDivisionOperators<T, T, T>
{
  public static readonly SpanningMeasure<T> Null = new NullSpanningMeasure<T>();

  public TariffMeasure<T> SpanMin()
  {
    {
      return this switch
      {
        MinMaxSpanningMeasure<T> minMax => minMax.TrueMin,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanMax()
  {
    {
      return this switch
      {
        MinMaxSpanningMeasure<T> minMax => minMax.TrueMax,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanAvg()
  {
    {
      return this switch
      {
        AvgSpanningMeasure<T> avg => avg.TrueAvg,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanPeak()
  {
    {
      return this switch
      {
        PeakSpanningMeasure<T> peak => peak.TruePeak,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanDiff()
  {
    return SpanMax().Subtract(SpanMin());
  }

  public SpanningMeasure<TConverted> ConvertPrimitiveTo<TConverted>()
    where TConverted : struct,
    IComparisonOperators<TConverted, TConverted, bool>,
    IAdditionOperators<TConverted, TConverted, TConverted>,
    ISubtractionOperators<TConverted, TConverted, TConverted>,
    IMultiplyOperators<TConverted, TConverted, TConverted>,
    IDivisionOperators<TConverted, TConverted, TConverted>
  {
    return this switch
    {
      MinMaxSpanningMeasure<T> minMax => new MinMaxSpanningMeasure<TConverted>(
        minMax.TrueMin.ConvertPrimitiveTo<TConverted>(),
        minMax.TrueMax.ConvertPrimitiveTo<TConverted>()),
      AvgSpanningMeasure<T> avg => new AvgSpanningMeasure<TConverted>(
        avg.TrueAvg.ConvertPrimitiveTo<TConverted>()),
      PeakSpanningMeasure<T> peak => new PeakSpanningMeasure<TConverted>(
        peak.TruePeak.ConvertPrimitiveTo<TConverted>()),
      _ => new NullSpanningMeasure<TConverted>()
    };
  }

  public TariffMeasure<T> SpanDifferential(T y)
  {
    return SpanDiff().Divide(y);
  }
}
