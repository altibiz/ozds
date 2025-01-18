using Ozds.Iot.Attributes;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Iot.Entities;

[MeterIdPrefix("schneider-iEM3xxx")]
public class PidgeonSchneideriEM3xxxMeterPushRequestEntity
  : IPidgeonMeterPushRequestEntity
{
  public string MeterId { get; set; } = default!;

  public DateTimeOffset Timestamp { get; set; } = default!;

  public PidgeonSchneideriEM3xxxMeterPushRequestData Data { get; set; }
    = default!;
}

public class PidgeonSchneideriEM3xxxMeterPushRequestData
{
  public decimal VoltageL1AnyT0_V { get; set; }

  public decimal VoltageL2AnyT0_V { get; set; }

  public decimal VoltageL3AnyT0_V { get; set; }

  public decimal CurrentL1AnyT0_A { get; set; }

  public decimal CurrentL2AnyT0_A { get; set; }

  public decimal CurrentL3AnyT0_A { get; set; }

  public decimal ActivePowerL1NetT0_W { get; set; }

  public decimal ActivePowerL2NetT0_W { get; set; }

  public decimal ActivePowerL3NetT0_W { get; set; }

  public decimal ReactivePowerTotalNetT0_VAR { get; set; }

  public decimal ApparentPowerTotalNetT0_VA { get; set; }

  public decimal ActiveEnergyL1ImportT0_Wh { get; set; }

  public decimal ActiveEnergyL2ImportT0_Wh { get; set; }

  public decimal ActiveEnergyL3ImportT0_Wh { get; set; }

  public decimal ActiveEnergyTotalImportT0_Wh { get; set; }

  public decimal ActiveEnergyTotalExportT0_Wh { get; set; }

  public decimal ReactiveEnergyTotalImportT0_VARh { get; set; }

  public decimal ReactiveEnergyTotalExportT0_VARh { get; set; }

  public decimal ActiveEnergyTotalImportT1_Wh { get; set; }

  public decimal ActiveEnergyTotalImportT2_Wh { get; set; }
}
