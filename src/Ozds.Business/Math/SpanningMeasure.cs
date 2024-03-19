using System.Numerics;

namespace Ozds.Business.Math;

public record MinMaxSpanningMeasure<T>(T TrueMin, T TrueMax)
  : SpanningMeasure<T>
  where T :
    ISubtractionOperators<T, T, T>,
    IMultiplyOperators<T, float, T>,
    IDivisionOperators<T, float, T>,
    new();

public record SpanningMeasure<T>() where T :
  ISubtractionOperators<T, T, T>,
  IMultiplyOperators<T, float, T>,
  IDivisionOperators<T, float, T>,
  new()
{
  public static readonly SpanningMeasure<T> Null = new();

  public T SpanMin => this switch
  {
    MinMaxSpanningMeasure<T> minMax => minMax.TrueMin,
    _ => new()
  };

  public T SpanMax => this switch
  {
    MinMaxSpanningMeasure<T> minMax => minMax.TrueMax,
    _ => new()
  };

  public T SpanDiff => SpanMax - SpanMin;

  public T SpanIntegral(float y) => SpanDiff * y;

  public T SpanDifferential(float y) => SpanDiff / y;
}
