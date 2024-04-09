using Ozds.Business.Iot;
using Ozds.Fake.Conversion.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Conversion;

public class AbbB2xMeasurementRecordPushRequestConverter
  : MeasurementRecordPushRequestConverter<AbbB2xMeasurementRecord, AbbB2xPushRequest>
{
  protected override AbbB2xPushRequest ConvertToPushRequest(AbbB2xMeasurementRecord record) =>
    new(
      record.VoltageL1AnyT0_V,
      record.VoltageL2AnyT0_V,
      record.VoltageL3AnyT0_V,
      record.CurrentL1AnyT0_A,
      record.CurrentL2AnyT0_A,
      record.CurrentL3AnyT0_A,
      record.ActivePowerL1NetT0_W,
      record.ActivePowerL2NetT0_W,
      record.ActivePowerL3NetT0_W,
      record.ReactivePowerL1NetT0_VAR,
      record.ReactivePowerL2NetT0_VAR,
      record.ReactivePowerL3NetT0_VAR,
      record.ActiveEnergyL1ImportT0_Wh,
      record.ActiveEnergyL2ImportT0_Wh,
      record.ActiveEnergyL3ImportT0_Wh,
      record.ActiveEnergyL1ExportT0_Wh,
      record.ActiveEnergyL2ExportT0_Wh,
      record.ActiveEnergyL3ExportT0_Wh,
      record.ReactiveEnergyL1ImportT0_VARh,
      record.ReactiveEnergyL2ImportT0_VARh,
      record.ReactiveEnergyL3ImportT0_VARh,
      record.ReactiveEnergyL1ExportT0_VARh,
      record.ReactiveEnergyL2ExportT0_VARh,
      record.ReactiveEnergyL3ExportT0_VARh,
      record.ActiveEnergyTotalImportT0_Wh,
      record.ActiveEnergyTotalExportT0_Wh,
      record.ReactiveEnergyTotalImportT0_VARh,
      record.ReactiveEnergyTotalExportT0_VARh,
      record.ActiveEnergyTotalImportT1_Wh,
      record.ActiveEnergyTotalImportT2_Wh
    );
}
