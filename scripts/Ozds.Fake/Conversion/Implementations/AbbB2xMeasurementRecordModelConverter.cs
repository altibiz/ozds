using Ozds.Business.Models;
using Ozds.Fake.Conversion.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Conversion.Implementations;

public class AbbB2xMeasurementModelConverter
  : MeasurementRecordModelConverter<
    AbbB2xMeasurementRecord,
    AbbB2xMeasurementModel>
{
  protected override AbbB2xMeasurementModel ConvertToModel(
    AbbB2xMeasurementRecord record)
  {
    return new AbbB2xMeasurementModel
    {
      MeterId = record.MeterId,
      MeasurementLocationId = record.MeasurementLocationId,
      Timestamp = record.Timestamp,
      VoltageL1AnyT0_V = record.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = record.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = record.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = record.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = record.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = record.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = record.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = record.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = record.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = record.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = record.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = record.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = record.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = record.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = record.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = record.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = record.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = record.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = record.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = record.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = record.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = record.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = record.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = record.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = record.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = record.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        record.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        record.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = record.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = record.ActiveEnergyTotalImportT2_Wh
    };
  }

  protected override AbbB2xMeasurementRecord ConvertToRecord(
    AbbB2xMeasurementModel model
  )
  {
    return new AbbB2xMeasurementRecord
    {
      MeterId = model.MeterId,
      MeasurementLocationId = model.MeasurementLocationId,
      Timestamp = model.Timestamp,
      VoltageL1AnyT0_V = model.VoltageL1AnyT0_V,
      VoltageL2AnyT0_V = model.VoltageL2AnyT0_V,
      VoltageL3AnyT0_V = model.VoltageL3AnyT0_V,
      CurrentL1AnyT0_A = model.CurrentL1AnyT0_A,
      CurrentL2AnyT0_A = model.CurrentL2AnyT0_A,
      CurrentL3AnyT0_A = model.CurrentL3AnyT0_A,
      ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W,
      ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W,
      ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W,
      ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR,
      ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR,
      ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR,
      ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh,
      ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh,
      ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh,
      ActiveEnergyL1ExportT0_Wh = model.ActiveEnergyL1ExportT0_Wh,
      ActiveEnergyL2ExportT0_Wh = model.ActiveEnergyL2ExportT0_Wh,
      ActiveEnergyL3ExportT0_Wh = model.ActiveEnergyL3ExportT0_Wh,
      ReactiveEnergyL1ImportT0_VARh = model.ReactiveEnergyL1ImportT0_VARh,
      ReactiveEnergyL2ImportT0_VARh = model.ReactiveEnergyL2ImportT0_VARh,
      ReactiveEnergyL3ImportT0_VARh = model.ReactiveEnergyL3ImportT0_VARh,
      ReactiveEnergyL1ExportT0_VARh = model.ReactiveEnergyL1ExportT0_VARh,
      ReactiveEnergyL2ExportT0_VARh = model.ReactiveEnergyL2ExportT0_VARh,
      ReactiveEnergyL3ExportT0_VARh = model.ReactiveEnergyL3ExportT0_VARh,
      ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh,
      ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh,
      ReactiveEnergyTotalImportT0_VARh =
        model.ReactiveEnergyTotalImportT0_VARh,
      ReactiveEnergyTotalExportT0_VARh =
        model.ReactiveEnergyTotalExportT0_VARh,
      ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh,
      ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh
    };
  }
}
