namespace Ozds.Business.Math;

public record class MinMaxSpanningMeasure(TariffMeasure TrueMin, TariffMeasure TrueMax)
  : SpanningMeasure;

public record class NullSpanningMeasure() : SpanningMeasure;

public abstract record class SpanningMeasure
{
  public static readonly SpanningMeasure Null = new NullSpanningMeasure();

  public TariffMeasure SpanMin
  {
    get
    {
      return this switch
      {
        MinMaxSpanningMeasure minMax => minMax.TrueMin,
        _ => TariffMeasure.Null
      };
    }
  }

  public TariffMeasure SpanMax
  {
    get
    {
      return this switch
      {
        MinMaxSpanningMeasure minMax => minMax.TrueMax,
        _ => TariffMeasure.Null
      };
    }
  }

  public TariffMeasure SpanDiff
  {
    get { return SpanMax - SpanMin; }
  }

  public TariffMeasure SpanIntegral(float y)
  {
    return SpanDiff * y;
  }

  public TariffMeasure SpanDifferential(float y)
  {
    return SpanDiff / y;
  }
}
