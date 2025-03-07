using Ozds.Client.Records.Abstractions;

namespace Ozds.Client.Records;

public class AbbB2xMeasurementRecord : MeasurementRecord, IMeasurementRecord
{
  public decimal VoltageL1AnyT0_V { get; set; } = default!;

  public decimal VoltageL2AnyT0_V { get; set; } = default!;

  public decimal VoltageL3AnyT0_V { get; set; } = default!;

  public decimal CurrentL1AnyT0_A { get; set; } = default!;

  public decimal CurrentL2AnyT0_A { get; set; } = default!;

  public decimal CurrentL3AnyT0_A { get; set; } = default!;

  public decimal ActivePowerL1NetT0_W { get; set; } = default!;

  public decimal ActivePowerL2NetT0_W { get; set; } = default!;

  public decimal ActivePowerL3NetT0_W { get; set; } = default!;

  public decimal ReactivePowerL1NetT0_VAR { get; set; } = default!;

  public decimal ReactivePowerL2NetT0_VAR { get; set; } = default!;

  public decimal ReactivePowerL3NetT0_VAR { get; set; } = default!;

  public decimal ActiveEnergyL1ImportT0_Wh { get; set; } = default!;

  public decimal ActiveEnergyL2ImportT0_Wh { get; set; } = default!;

  public decimal ActiveEnergyL3ImportT0_Wh { get; set; } = default!;

  public decimal ActiveEnergyL1ExportT0_Wh { get; set; } = default!;

  public decimal ActiveEnergyL2ExportT0_Wh { get; set; } = default!;

  public decimal ActiveEnergyL3ExportT0_Wh { get; set; } = default!;

  public decimal ReactiveEnergyL1ImportT0_VARh { get; set; } = default!;

  public decimal ReactiveEnergyL2ImportT0_VARh { get; set; } = default!;

  public decimal ReactiveEnergyL3ImportT0_VARh { get; set; } = default!;

  public decimal ReactiveEnergyL1ExportT0_VARh { get; set; } = default!;

  public decimal ReactiveEnergyL2ExportT0_VARh { get; set; } = default!;

  public decimal ReactiveEnergyL3ExportT0_VARh { get; set; } = default!;

  public decimal ActiveEnergyTotalImportT0_Wh { get; set; } = default!;

  public decimal ActiveEnergyTotalExportT0_Wh { get; set; } = default!;

  public decimal ReactiveEnergyTotalImportT0_VARh { get; set; } = default!;

  public decimal ReactiveEnergyTotalExportT0_VARh { get; set; } = default!;

  public decimal ActiveEnergyTotalImportT1_Wh { get; set; } = default!;

  public decimal ActiveEnergyTotalImportT2_Wh { get; set; } = default!;
}
