using Ozds.Fake.Records.Base;

namespace Ozds.Fake.Records;

public class AbbB2xMeasurementRecord : MeasurementRecord
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
  public required float ReactivePowerL1NetT0_VAR { get; set; }
  public required float ReactivePowerL2NetT0_VAR { get; set; }
  public required float ReactivePowerL3NetT0_VAR { get; set; }
  public required float ActiveEnergyL1ImportT0_Wh { get; set; }
  public required float ActiveEnergyL2ImportT0_Wh { get; set; }
  public required float ActiveEnergyL3ImportT0_Wh { get; set; }
  public required float ActiveEnergyL1ExportT0_Wh { get; set; }
  public required float ActiveEnergyL2ExportT0_Wh { get; set; }
  public required float ActiveEnergyL3ExportT0_Wh { get; set; }
  public required float ReactiveEnergyL1ImportT0_VARh { get; set; }
  public required float ReactiveEnergyL2ImportT0_VARh { get; set; }
  public required float ReactiveEnergyL3ImportT0_VARh { get; set; }
  public required float ReactiveEnergyL1ExportT0_VARh { get; set; }
  public required float ReactiveEnergyL2ExportT0_VARh { get; set; }
  public required float ReactiveEnergyL3ExportT0_VARh { get; set; }
  public required float ActiveEnergyTotalImportT0_Wh { get; set; }
  public required float ActiveEnergyTotalExportT0_Wh { get; set; }
  public required float ReactiveEnergyTotalImportT0_VARh { get; set; }
  public required float ReactiveEnergyTotalExportT0_VARh { get; set; }
  public required float ActiveEnergyTotalImportT1_Wh { get; set; }
  public required float ActiveEnergyTotalImportT2_Wh { get; set; }
#pragma warning restore CA1707
}
