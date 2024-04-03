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
