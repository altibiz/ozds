using Ozds.Business.Math;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate : IMeasurement
{
  public IntervalModel Interval { get; }

  public long Count { get; }

  public long QuarterHourCount { get; }

  public SpanningMeasure<decimal> ActiveEnergySpan_Wh { get; }

  public SpanningMeasure<decimal> ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure<decimal> ApparentEnergySpan_VAh { get; }
}
