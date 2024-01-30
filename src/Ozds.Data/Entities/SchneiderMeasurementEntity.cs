namespace Ozds.Data.Entities;

public class SchneiderMeasurementEntity : MeasurementEntity
{
#pragma warning disable CA1707
  public float VoltageL1_V { get; set; } = default!;
  public float VoltageL2_V { get; set; } = default!;
  public float VoltageL3_V { get; set; } = default!;
  public float CurrentL1_A { get; set; } = default!;
  public float CurrentL2_A { get; set; } = default!;
  public float CurrentL3_A { get; set; } = default!;
  public float ActivePowerL1_W { get; set; } = default!;
  public float ActivePowerL2_W { get; set; } = default!;
  public float ActivePowerL3_W { get; set; } = default!;
  public float ReactivePowerTotal_VAR { get; set; } = default!;
  public float ApparentPowerTotal_VA { get; set; } = default!;
  public float ActiveEnergyImportL1_Wh { get; set; } = default!;
  public float ActiveEnergyImportL2_Wh { get; set; } = default!;
  public float ActiveEnergyImportL3_Wh { get; set; } = default!;
  public float ActiveEnergyImportTotal_Wh { get; set; } = default!;
  public float ActiveEnergyExportTotal_Wh { get; set; } = default!;
  public float ReactiveEnergyImportTotal_VARh { get; set; } = default!;
  public float ReactiveEnergyExportTotal_VARh { get; set; } = default!;
#pragma warning disable CA1707
}
