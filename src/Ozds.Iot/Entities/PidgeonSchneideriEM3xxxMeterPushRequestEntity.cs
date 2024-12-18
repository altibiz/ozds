using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

public record PidgeonSchneideriEM3xxxMeterPushRequestEntity(
  string MeterId,
  DateTimeOffset Timestamp,
  PidgeonSchneideriEM3xxxMeterPushRequestData Data
) : IPidgeonMeterPushRequestEntity
{
  public static string MeterIdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }
}

public record PidgeonSchneideriEM3xxxMeterPushRequestData(
  decimal VoltageL1AnyT0_V,
  decimal VoltageL2AnyT0_V,
  decimal VoltageL3AnyT0_V,
  decimal CurrentL1AnyT0_A,
  decimal CurrentL2AnyT0_A,
  decimal CurrentL3AnyT0_A,
  decimal ActivePowerL1NetT0_W,
  decimal ActivePowerL2NetT0_W,
  decimal ActivePowerL3NetT0_W,
  decimal ReactivePowerTotalNetT0_VAR,
  decimal ApparentPowerTotalNetT0_VA,
  decimal ActiveEnergyL1ImportT0_Wh,
  decimal ActiveEnergyL2ImportT0_Wh,
  decimal ActiveEnergyL3ImportT0_Wh,
  decimal ActiveEnergyTotalImportT0_Wh,
  decimal ActiveEnergyTotalExportT0_Wh,
  decimal ReactiveEnergyTotalImportT0_VARh,
  decimal ReactiveEnergyTotalExportT0_VARh,
  decimal ActiveEnergyTotalImportT1_Wh,
  decimal ActiveEnergyTotalImportT2_Wh
);
