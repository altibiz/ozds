using Ozds.Business.Math;

namespace Ozds.Business.Models.Abstractions;

public abstract record MeasurementModel(
  string MeterId,
  DateTimeOffset Timestamp
) : IMeasurement
{
  public abstract TariffMeasure Current_A { get; }

  public abstract TariffMeasure Voltage_V { get; }

  public abstract TariffMeasure ActivePower_W { get; }

  public abstract TariffMeasure ReactivePower_VAR { get; }

  public abstract TariffMeasure ApparentPower_VA { get; }

  public abstract TariffMeasure ActiveEnergyCumulative_Wh { get; }

  public abstract TariffMeasure ReactiveEnergyCumulative_VARh { get; }

  public abstract TariffMeasure ApparentEnergyCumulative_VAh { get; }
}
