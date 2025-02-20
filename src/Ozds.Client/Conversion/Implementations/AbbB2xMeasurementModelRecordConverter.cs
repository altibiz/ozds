using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Client.Conversion.Implementations;

public class AbbB2xMeasurementModelRecordConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelRecordConverter<
    AbbB2xMeasurementModel,
    MeasurementModel,
    AbbB2xMeasurementRecord,
    MeasurementRecord
  >(serviceProvider)
{
  public override void InitializeRecord(
    AbbB2xMeasurementModel model,
    AbbB2xMeasurementRecord record
  )
  {
    base.InitializeRecord(model, record);
    record.VoltageL1AnyT0_V = model.VoltageL1AnyT0_V;
    record.VoltageL2AnyT0_V = model.VoltageL2AnyT0_V;
    record.VoltageL3AnyT0_V = model.VoltageL3AnyT0_V;
    record.CurrentL1AnyT0_A = model.CurrentL1AnyT0_A;
    record.CurrentL2AnyT0_A = model.CurrentL2AnyT0_A;
    record.CurrentL3AnyT0_A = model.CurrentL3AnyT0_A;
    record.ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W;
    record.ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W;
    record.ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W;
    record.ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR;
    record.ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR;
    record.ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR;
    record.ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh;
    record.ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh;
    record.ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh;
    record.ActiveEnergyL1ExportT0_Wh = model.ActiveEnergyL1ExportT0_Wh;
    record.ActiveEnergyL2ExportT0_Wh = model.ActiveEnergyL2ExportT0_Wh;
    record.ActiveEnergyL3ExportT0_Wh = model.ActiveEnergyL3ExportT0_Wh;
    record.ReactiveEnergyL1ImportT0_VARh = model.ReactiveEnergyL1ImportT0_VARh;
    record.ReactiveEnergyL2ImportT0_VARh = model.ReactiveEnergyL2ImportT0_VARh;
    record.ReactiveEnergyL3ImportT0_VARh = model.ReactiveEnergyL3ImportT0_VARh;
    record.ReactiveEnergyL1ExportT0_VARh = model.ReactiveEnergyL1ExportT0_VARh;
    record.ReactiveEnergyL2ExportT0_VARh = model.ReactiveEnergyL2ExportT0_VARh;
    record.ReactiveEnergyL3ExportT0_VARh = model.ReactiveEnergyL3ExportT0_VARh;
    record.ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh;
    record.ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh;
    record.ReactiveEnergyTotalImportT0_VARh =
      model.ReactiveEnergyTotalImportT0_VARh;
    record.ReactiveEnergyTotalExportT0_VARh =
      model.ReactiveEnergyTotalExportT0_VARh;
    record.ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh;
    record.ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh;
  }

  public override void InitializeModel(
    AbbB2xMeasurementRecord record,
    AbbB2xMeasurementModel model
  )
  {
    base.InitializeModel(record, model);
    model.VoltageL1AnyT0_V = record.VoltageL1AnyT0_V;
    model.VoltageL2AnyT0_V = record.VoltageL2AnyT0_V;
    model.VoltageL3AnyT0_V = record.VoltageL3AnyT0_V;
    model.CurrentL1AnyT0_A = record.CurrentL1AnyT0_A;
    model.CurrentL2AnyT0_A = record.CurrentL2AnyT0_A;
    model.CurrentL3AnyT0_A = record.CurrentL3AnyT0_A;
    model.ActivePowerL1NetT0_W = record.ActivePowerL1NetT0_W;
    model.ActivePowerL2NetT0_W = record.ActivePowerL2NetT0_W;
    model.ActivePowerL3NetT0_W = record.ActivePowerL3NetT0_W;
    model.ReactivePowerL1NetT0_VAR = record.ReactivePowerL1NetT0_VAR;
    model.ReactivePowerL2NetT0_VAR = record.ReactivePowerL2NetT0_VAR;
    model.ReactivePowerL3NetT0_VAR = record.ReactivePowerL3NetT0_VAR;
    model.ActiveEnergyL1ImportT0_Wh = record.ActiveEnergyL1ImportT0_Wh;
    model.ActiveEnergyL2ImportT0_Wh = record.ActiveEnergyL2ImportT0_Wh;
    model.ActiveEnergyL3ImportT0_Wh = record.ActiveEnergyL3ImportT0_Wh;
    model.ActiveEnergyL1ExportT0_Wh = record.ActiveEnergyL1ExportT0_Wh;
    model.ActiveEnergyL2ExportT0_Wh = record.ActiveEnergyL2ExportT0_Wh;
    model.ActiveEnergyL3ExportT0_Wh = record.ActiveEnergyL3ExportT0_Wh;
    model.ReactiveEnergyL1ImportT0_VARh = record.ReactiveEnergyL1ImportT0_VARh;
    model.ReactiveEnergyL2ImportT0_VARh = record.ReactiveEnergyL2ImportT0_VARh;
    model.ReactiveEnergyL3ImportT0_VARh = record.ReactiveEnergyL3ImportT0_VARh;
    model.ReactiveEnergyL1ExportT0_VARh = record.ReactiveEnergyL1ExportT0_VARh;
    model.ReactiveEnergyL2ExportT0_VARh = record.ReactiveEnergyL2ExportT0_VARh;
    model.ReactiveEnergyL3ExportT0_VARh = record.ReactiveEnergyL3ExportT0_VARh;
    model.ActiveEnergyTotalImportT0_Wh = record.ActiveEnergyTotalImportT0_Wh;
    model.ActiveEnergyTotalExportT0_Wh = record.ActiveEnergyTotalExportT0_Wh;
    model.ReactiveEnergyTotalImportT0_VARh =
      record.ReactiveEnergyTotalImportT0_VARh;
    model.ReactiveEnergyTotalExportT0_VARh =
      record.ReactiveEnergyTotalExportT0_VARh;
    model.ActiveEnergyTotalImportT1_Wh = record.ActiveEnergyTotalImportT1_Wh;
    model.ActiveEnergyTotalImportT2_Wh = record.ActiveEnergyTotalImportT2_Wh;
  }
}
