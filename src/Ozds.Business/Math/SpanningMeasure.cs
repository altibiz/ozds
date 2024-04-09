using System.Numerics;

namespace Ozds.Business.Math;

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

  public TariffMeasure<T> SpanMin
  {
    get
    {
      return this switch
      {
        MinMaxSpanningMeasure<T> minMax => minMax.TrueMin,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanMax
  {
    get
    {
      return this switch
      {
        MinMaxSpanningMeasure<T> minMax => minMax.TrueMax,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanAvg
  {
    get
    {
      return this switch
      {
        AvgSpanningMeasure<T> avg => avg.TrueAvg,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanPeak
  {
    get
    {
      return this switch
      {
        PeakSpanningMeasure<T> peak => peak.TruePeak,
        _ => TariffMeasure<T>.Null
      };
    }
  }

  public TariffMeasure<T> SpanDiff
  {
    get { return SpanMax - SpanMin; }
  }

  public SpanningMeasure<U> ConvertPrimitiveTo<U>()
    where U : struct,
    IComparisonOperators<U, U, bool>,
    IAdditionOperators<U, U, U>,
    ISubtractionOperators<U, U, U>,
    IMultiplyOperators<U, U, U>,
    IDivisionOperators<U, U, U>
  {
    return this switch
    {
      MinMaxSpanningMeasure<T> minMax => new MinMaxSpanningMeasure<U>(
        minMax.TrueMin.ConvertPrimitiveTo<U>(),
        minMax.TrueMax.ConvertPrimitiveTo<U>()),
      AvgSpanningMeasure<T> avg => new AvgSpanningMeasure<U>(
        avg.TrueAvg.ConvertPrimitiveTo<U>()),
      PeakSpanningMeasure<T> peak => new PeakSpanningMeasure<U>(
        peak.TruePeak.ConvertPrimitiveTo<U>()),
      _ => new NullSpanningMeasure<U>()
    };
  }

  public TariffMeasure<T> SpanIntegral(T y)
  {
    return this switch
    {
      MinMaxSpanningMeasure<T> minMax => minMax.TrueMax - minMax.TrueMin,
      AvgSpanningMeasure<T> avg => avg.TrueAvg * y,
      _ => TariffMeasure<T>.Null
    };
  }

  public TariffMeasure<T> SpanDifferential(T y)
  {
    return SpanDiff / y;
  }
}
