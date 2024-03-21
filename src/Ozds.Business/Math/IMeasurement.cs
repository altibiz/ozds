namespace Ozds.Business.Math;

public interface IMeasurement
{
  public string Source { get; }

  public DateTimeOffset Timestamp { get; }

  public TariffMeasure Current_A { get; }

  public TariffMeasure Voltage_V { get; }

  public TariffMeasure ActivePower_W { get; }

  public TariffMeasure ReactivePower_VAR { get; }

  public TariffMeasure ApparentPower_VA { get; }

  public TariffMeasure ActiveEnergyCumulative_Wh { get; }

  public TariffMeasure ReactiveEnergyCumulative_VARh { get; }

  public TariffMeasure ApparentEnergyCumulative_VAh { get; }
}
