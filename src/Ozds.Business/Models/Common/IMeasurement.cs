namespace Ozds.Business.Models.Common;

public interface IMeasurement
{
  public string Source { get; }

  public DateTimeOffset Timestamp { get; }

  public PhasicMeasure Current_A { get; }

  public PhasicMeasure Voltage_V { get; }

  public PhasicMeasure ActivePower_W { get; }

  public PhasicMeasure ReactivePower_VAR { get; }

  public PhasicMeasure ApparentPower_VA { get; }

  public CumulativeMeasure ActiveEnergyTotal_Wh { get; }

  public CumulativeMeasure ReactiveEnergyTotal_VARh { get; }

  public CumulativeMeasure ApparentEnergyTotal_VAh { get; }
}
