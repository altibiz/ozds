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
