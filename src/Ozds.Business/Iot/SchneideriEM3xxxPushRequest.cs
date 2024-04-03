using Ozds.Business.Models;

namespace Ozds.Business.Iot;

public record SchneideriEM3xxxPushRequest(
  float VoltageL1AnyT0_V,
  float VoltageL2AnyT0_V,
  float VoltageL3AnyT0_V,
  float CurrentL1AnyT0_A,
  float CurrentL2AnyT0_A,
  float CurrentL3AnyT0_A,
  float ActivePowerL1NetT0_W,
  float ActivePowerL2NetT0_W,
  float ActivePowerL3NetT0_W,
  float ReactivePowerTotalNetT0_VAR,
  float ApparentPowerTotalNetT0_VA,
  float ActiveEnergyL1ImportT0_Wh,
  float ActiveEnergyL2ImportT0_Wh,
  float ActiveEnergyL3ImportT0_Wh,
  float ActiveEnergyTotalImportT0_Wh,
  float ActiveEnergyTotalExportT0_Wh,
  float ReactiveEnergyTotalImportT0_VARh,
  float ReactiveEnergyTotalExportT0_VARh,
  float ActiveEnergyTotalImportT1_Wh,
  float ActiveEnergyTotalImportT2_Wh
);

public static class SchneideriEM3xxxPushRequestExtensions
{
  public static SchneideriEM3xxxMeasurementModel ToModel(
    this SchneideriEM3xxxPushRequest request,
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
      ReactivePowerTotalNetT0_VAR = request.ReactivePowerTotalNetT0_VAR,
      ApparentPowerTotalNetT0_VA = request.ApparentPowerTotalNetT0_VA,
      ActiveEnergyL1ImportT0_Wh = request.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = request.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = request.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyTotalImportT0_Wh = request.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = request.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh = request.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh = request.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = request.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = request.ActiveEnergyTotalImportT2_Wh
    };
}
