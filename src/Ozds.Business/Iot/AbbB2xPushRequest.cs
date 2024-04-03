using Ozds.Business.Models;

namespace Ozds.Business.Iot;

public record AbbB2xPushRequest(
  float VoltageL1AnyT0_V,
  float VoltageL2AnyT0_V,
  float VoltageL3AnyT0_V,
  float CurrentL1AnyT0_A,
  float CurrentL2AnyT0_A,
  float CurrentL3AnyT0_A,
  float ActivePowerL1NetT0_W,
  float ActivePowerL2NetT0_W,
  float ActivePowerL3NetT0_W,
  float ReactivePowerL1NetT0_VAR,
  float ReactivePowerL2NetT0_VAR,
  float ReactivePowerL3NetT0_VAR,
  float ActiveEnergyL1ImportT0_Wh,
  float ActiveEnergyL2ImportT0_Wh,
  float ActiveEnergyL3ImportT0_Wh,
  float ActiveEnergyL1ExportT0_Wh,
  float ActiveEnergyL2ExportT0_Wh,
  float ActiveEnergyL3ExportT0_Wh,
  float ReactiveEnergyL1ImportT0_VARh,
  float ReactiveEnergyL2ImportT0_VARh,
  float ReactiveEnergyL3ImportT0_VARh,
  float ReactiveEnergyL1ExportT0_VARh,
  float ReactiveEnergyL2ExportT0_VARh,
  float ReactiveEnergyL3ExportT0_VARh,
  float ActiveEnergyTotalImportT0_Wh,
  float ActiveEnergyTotalExportT0_Wh,
  float ReactiveEnergyTotalImportT0_VARh,
  float ReactiveEnergyTotalExportT0_VARh,
  float ActiveEnergyTotalImportT1_Wh,
  float ActiveEnergyTotalImportT2_Wh
);

public static class AbbB2xPushRequestExtensions
{
  public static AbbB2xMeasurementModel ToModel(
    this AbbB2xPushRequest request,
    string meterId,
    DateTimeOffset timestamp
  ) =>
    new()
    {
      MeterId = meterId,
      Timestamp = timestamp,
      VoltageL1AnyT0_V = request.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = request.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = request.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = request.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = request.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = request.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = request.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = request.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = request.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = request.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = request.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = request.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = request.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = request.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = request.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = request.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = request.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = request.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = request.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = request.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = request.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = request.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = request.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = request.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = request.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = request.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = request.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = request.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = request.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = request.ActiveEnergyTotalImportT2_Wh
    };
}
