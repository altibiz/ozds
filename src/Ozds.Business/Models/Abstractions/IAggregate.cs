using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate : IMeasurement
{
  public TimeSpan Interval { get; }

  public long Count { get; }

  public SpanningMeasure ActiveEnergySpan_Wh { get; }

  public SpanningMeasure ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure ApparentEnergySpan_VAh { get; }

  TariffMeasure IMeasurement.ActivePower_W
  {
    get
    {
      return ActiveEnergySpan_Wh.SpanDifferential((float)Interval.TotalHours);
    }
  }

  TariffMeasure IMeasurement.ReactivePower_VAR
  {
    get
    {
      return ReactiveEnergySpan_VARh.SpanDifferential(
        (float)Interval.TotalHours);
    }
  }

  TariffMeasure IMeasurement.ApparentPower_VA
  {
    get
    {
      return ApparentEnergySpan_VAh.SpanDifferential(
        (float)Interval.TotalHours);
    }
  }

  TariffMeasure IMeasurement.ActiveEnergyCumulative_Wh
  {
    get { return ActiveEnergySpan_Wh.SpanMax; }
  }

  TariffMeasure IMeasurement.ReactiveEnergyCumulative_VARh
  {
    get { return ReactiveEnergySpan_VARh.SpanMax; }
  }

  TariffMeasure IMeasurement.ApparentEnergyCumulative_VAh
  {
    get { return ApparentEnergySpan_VAh.SpanMax; }
  }
}
