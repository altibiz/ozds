using Ozds.Fake.Records.Base;

namespace Ozds.Fake.Records;

public class SchneideriEM3xxxMeasurementRecord : MeasurementRecord
{
#pragma warning disable CA1707
  public required float VoltageL1AnyT0_V { get; set; }
  public required float VoltageL2AnyT0_V { get; set; }
  public required float VoltageL3AnyT0_V { get; set; }
  public required float CurrentL1AnyT0_A { get; set; }
  public required float CurrentL2AnyT0_A { get; set; }
  public required float CurrentL3AnyT0_A { get; set; }
  public required float ActivePowerL1NetT0_W { get; set; }
  public required float ActivePowerL2NetT0_W { get; set; }
  public required float ActivePowerL3NetT0_W { get; set; }
  public required float ReactivePowerTotalNetT0_VAR { get; set; }
  public required float ApparentPowerTotalNetT0_VA { get; set; }
  public required float ActiveEnergyL1ImportT0_Wh { get; set; }
  public required float ActiveEnergyL2ImportT0_Wh { get; set; }
  public required float ActiveEnergyL3ImportT0_Wh { get; set; }
  public required float ActiveEnergyTotalImportT0_Wh { get; set; }
  public required float ActiveEnergyTotalExportT0_Wh { get; set; }
  public required float ReactiveEnergyTotalImportT0_VARh { get; set; }
  public required float ReactiveEnergyTotalExportT0_VARh { get; set; }
  public required float ActiveEnergyTotalImportT1_Wh { get; set; }
  public required float ActiveEnergyTotalImportT2_Wh { get; set; }
#pragma warning restore CA1707
}
