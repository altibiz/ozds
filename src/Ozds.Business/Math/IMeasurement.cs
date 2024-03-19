namespace Ozds.Business.Math;

public interface IMeasurement
{
  public string Source { get; }

  public DateTimeOffset Timestamp { get; }

  public DuplexMeasure Current_A { get; }

  public DuplexMeasure Voltage_V { get; }

  public DuplexMeasure ActivePower_W { get; }

  public DuplexMeasure ReactivePower_VAR { get; }

  public DuplexMeasure ApparentPower_VA { get; }

  public DuplexMeasure ActiveEnergyCumulative_Wh { get; }

  public DuplexMeasure ReactiveEnergyCumulative_VARh { get; }

  public DuplexMeasure ApparentEnergyCumulative_VAh { get; }

  public TariffMeasure TariffEnergyCumulative_Wh { get; }
}
