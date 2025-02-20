using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Client.Conversion.Base;
using Ozds.Client.Records;

namespace Ozds.Client.Conversion.Implementations;

public class SchneideriEM3xxxMeasurementModelRecordConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelRecordConverter<
    SchneideriEM3xxxMeasurementModel,
    MeasurementModel,
    SchneideriEM3xxxMeasurementRecord,
    MeasurementRecord
  >(serviceProvider)
{
  public override void InitializeRecord(
    SchneideriEM3xxxMeasurementModel model,
    SchneideriEM3xxxMeasurementRecord record
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
    record.ReactivePowerTotalNetT0_VAR = model.ReactivePowerTotalNetT0_VAR;
    record.ApparentPowerTotalNetT0_VA = model.ApparentPowerTotalNetT0_VA;
    record.ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh;
    record.ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh;
    record.ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh;
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
    SchneideriEM3xxxMeasurementRecord record,
    SchneideriEM3xxxMeasurementModel model
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
    model.ReactivePowerTotalNetT0_VAR = record.ReactivePowerTotalNetT0_VAR;
    model.ApparentPowerTotalNetT0_VA = record.ApparentPowerTotalNetT0_VA;
    model.ActiveEnergyL1ImportT0_Wh = record.ActiveEnergyL1ImportT0_Wh;
    model.ActiveEnergyL2ImportT0_Wh = record.ActiveEnergyL2ImportT0_Wh;
    model.ActiveEnergyL3ImportT0_Wh = record.ActiveEnergyL3ImportT0_Wh;
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
