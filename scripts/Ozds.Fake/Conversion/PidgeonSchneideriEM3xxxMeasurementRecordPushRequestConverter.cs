using System.Diagnostics.Metrics;
using Ozds.Fake.Conversion.Base;
using Ozds.Fake.Records;
using Ozds.Iot.Entities;

namespace Ozds.Fake.Conversion;

public class SchneideriEM3xxxMeasurementRecordPushRequestConverter
  : PidgeonMeasurementRecordPushRequestConverter<SchneideriEM3xxxMeasurementRecord,
    PidgeonSchneideriEM3xxxMeterPushRequestEntity>
{
  protected override PidgeonSchneideriEM3xxxMeterPushRequestEntity ConvertToPushRequest(
    SchneideriEM3xxxMeasurementRecord record)
  {
    return new PidgeonSchneideriEM3xxxMeterPushRequestEntity(
      MeterId: record.MeterId,
      Timestamp: record.Timestamp,
      Data: new PidgeonSchneideriEM3xxxMeterPushRequestData(
        record.VoltageL1AnyT0_V,
        record.VoltageL2AnyT0_V,
        record.VoltageL3AnyT0_V,
        record.CurrentL1AnyT0_A,
        record.CurrentL2AnyT0_A,
        record.CurrentL3AnyT0_A,
        record.ActivePowerL1NetT0_W,
        record.ActivePowerL2NetT0_W,
        record.ActivePowerL3NetT0_W,
        record.ReactivePowerTotalNetT0_VAR,
        record.ApparentPowerTotalNetT0_VA,
        record.ActiveEnergyL1ImportT0_Wh,
        record.ActiveEnergyL2ImportT0_Wh,
        record.ActiveEnergyL3ImportT0_Wh,
        record.ActiveEnergyTotalImportT0_Wh,
        record.ActiveEnergyTotalExportT0_Wh,
        record.ReactiveEnergyTotalImportT0_VARh,
        record.ReactiveEnergyTotalExportT0_VARh,
        record.ActiveEnergyTotalImportT1_Wh,
        record.ActiveEnergyTotalImportT2_Wh
      )
    );
  }
}
