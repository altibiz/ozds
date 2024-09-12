using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonAbbB2xPushRequestDataEntity(
  string MeterId,
  DateTimeOffset Timestamp,
  PidgeonAbbB2xPushRequestData Data
) : IPidgeonMeterPushRequestEntity
{
  public static string MeterIdPrefix => "abb-B2x";
}

public record PidgeonAbbB2xPushRequestData(
  decimal VoltageL1AnyT0_V,
  decimal VoltageL2AnyT0_V,
  decimal VoltageL3AnyT0_V,
  decimal CurrentL1AnyT0_A,
  decimal CurrentL2AnyT0_A,
  decimal CurrentL3AnyT0_A,
  decimal ActivePowerL1NetT0_W,
  decimal ActivePowerL2NetT0_W,
  decimal ActivePowerL3NetT0_W,
  decimal ReactivePowerL1NetT0_VAR,
  decimal ReactivePowerL2NetT0_VAR,
  decimal ReactivePowerL3NetT0_VAR,
  decimal ActiveEnergyL1ImportT0_Wh,
  decimal ActiveEnergyL2ImportT0_Wh,
  decimal ActiveEnergyL3ImportT0_Wh,
  decimal ActiveEnergyL1ExportT0_Wh,
  decimal ActiveEnergyL2ExportT0_Wh,
  decimal ActiveEnergyL3ExportT0_Wh,
  decimal ReactiveEnergyL1ImportT0_VARh,
  decimal ReactiveEnergyL2ImportT0_VARh,
  decimal ReactiveEnergyL3ImportT0_VARh,
  decimal ReactiveEnergyL1ExportT0_VARh,
  decimal ReactiveEnergyL2ExportT0_VARh,
  decimal ReactiveEnergyL3ExportT0_VARh,
  decimal ActiveEnergyTotalImportT0_Wh,
  decimal ActiveEnergyTotalExportT0_Wh,
  decimal ReactiveEnergyTotalImportT0_VARh,
  decimal ReactiveEnergyTotalExportT0_VARh,
  decimal ActiveEnergyTotalImportT1_Wh,
  decimal ActiveEnergyTotalImportT2_Wh
);
