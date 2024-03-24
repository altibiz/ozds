using Ozds.Business.Math;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Models.Abstractions;

public interface IAggregate
{
  public string MeterId { get; }

  public DateTimeOffset Timestamp { get; }

  public IntervalModel Interval { get; }

  public long Count { get; }

  public TariffMeasure Current_A { get; }

  public TariffMeasure Voltage_V { get; }

  public TariffMeasure ActivePower_W { get; }

  public TariffMeasure ReactivePower_VAR { get; }

  public TariffMeasure ApparentPower_VA { get; }

  public TariffMeasure ActiveEnergy_Wh { get; }

  public TariffMeasure ReactiveEnergy_VARh { get; }

  public TariffMeasure ApparentEnergy_VAh { get; }

  public SpanningMeasure ActiveEnergySpan_Wh { get; }

  public SpanningMeasure ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure ApparentEnergySpan_VAh { get; }
}
