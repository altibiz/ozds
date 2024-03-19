using System.Numerics;

namespace Ozds.Business.Math;

public record MinMaxSpanningMeasure<T>(T TrueMin, T TrueMax)
  : SpanningMeasure<T>
  where T :
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, float, T>,
  IDivisionOperators<T, float, T>,
  new();

public record SpanningMeasure<T> where T :
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, float, T>,
  IDivisionOperators<T, float, T>,
  new()
{
  public static readonly SpanningMeasure<T> Null = new();

  public T SpanMin
  {
    get
    {
      return this switch
      {
        MinMaxSpanningMeasure<T> minMax => minMax.TrueMin,
        _ => new T()
      };
    }
  }

  public T SpanMax
  {
    get
    {
      return this switch
      {
        MinMaxSpanningMeasure<T> minMax => minMax.TrueMax,
        _ => new T()
      };
    }
  }

  public T SpanDiff
  {
    get { return SpanMax - SpanMin; }
  }

  public T SpanIntegral(float y)
  {
    return SpanDiff * y;
  }

  public T SpanDifferential(float y)
  {
    return SpanDiff / y;
  }
}
