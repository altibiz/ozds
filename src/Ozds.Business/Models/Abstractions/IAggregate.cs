using Ozds.Business.Math;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate : IMeasurement
{
  public IntervalModel Interval { get; }

  public long Count { get; }

  public long QuarterHourCount { get; }

  public SpanningMeasure<decimal> VoltageSpan_V { get; }

  public SpanningMeasure<decimal> CurrentSpan_A { get; }

  public SpanningMeasure<decimal> ActivePowerSpan_W { get; }

  public SpanningMeasure<decimal> ReactivePowerSpan_VAR { get; }

  public SpanningMeasure<decimal> ApparentPowerSpan_VA { get; }

  public SpanningMeasure<decimal> ActiveEnergySpan_Wh { get; }

  public SpanningMeasure<decimal> DerivedActivePowerSpan_W { get; }

  public SpanningMeasure<decimal> ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure<decimal> DerivedReactivePowerSpan_VAR { get; }

  public SpanningMeasure<decimal> ApparentEnergySpan_VAh { get; }

  public SpanningMeasure<decimal> DerivedApparentPowerSpan_VA { get; }
}
