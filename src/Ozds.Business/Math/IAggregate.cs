namespace Ozds.Business.Math;

public interface IAggregate : IMeasurement
{
  public TimeSpan TimeSpan { get; }

  public long AggregateCount { get; }

  DuplexMeasure IMeasurement.ActivePower_W => ActiveEnergySpan_Wh.SpanDifferential((float)TimeSpan.TotalHours);

  DuplexMeasure IMeasurement.ReactivePower_VAR => ReactiveEnergySpan_VARh.SpanDifferential((float)TimeSpan.TotalHours);

  DuplexMeasure IMeasurement.ApparentPower_VA => ApparentEnergySpan_VAh.SpanDifferential((float)TimeSpan.TotalHours);

  DuplexMeasure IMeasurement.ActiveEnergyCumulative_Wh => ActiveEnergySpan_Wh.SpanMax;

  DuplexMeasure IMeasurement.ReactiveEnergyCumulative_VARh => ReactiveEnergySpan_VARh.SpanMax;

  DuplexMeasure IMeasurement.ApparentEnergyCumulative_VAh => ApparentEnergySpan_VAh.SpanMax;

  TariffMeasure IMeasurement.TariffEnergyCumulative_Wh => TariffEnergySpan_Wh.SpanMax;

  public SpanningMeasure<DuplexMeasure> ActiveEnergySpan_Wh { get; }

  public SpanningMeasure<DuplexMeasure> ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure<DuplexMeasure> ApparentEnergySpan_VAh { get; }

  public SpanningMeasure<TariffMeasure> TariffEnergySpan_Wh { get; }
}
