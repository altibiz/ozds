using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xMeasurementModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      AbbB2xMeasurementModel,
      MeasurementModel,
      AbbB2xMeasurementEntity,
      MeasurementEntity>(serviceProvider)
{
  public override void InitializeEntity(
    AbbB2xMeasurementModel model,
    AbbB2xMeasurementEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.VoltageL1AnyT0_V = model.VoltageL1AnyT0_V.ToFloat();
    entity.VoltageL2AnyT0_V = model.VoltageL2AnyT0_V.ToFloat();
    entity.VoltageL3AnyT0_V = model.VoltageL3AnyT0_V.ToFloat();
    entity.CurrentL1AnyT0_A = model.CurrentL1AnyT0_A.ToFloat();
    entity.CurrentL2AnyT0_A = model.CurrentL2AnyT0_A.ToFloat();
    entity.CurrentL3AnyT0_A = model.CurrentL3AnyT0_A.ToFloat();
    entity.ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W.ToFloat();
    entity.ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W.ToFloat();
    entity.ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W.ToFloat();
    entity.ReactivePowerL1NetT0_VAR = model.ReactivePowerL1NetT0_VAR.ToFloat();
    entity.ReactivePowerL2NetT0_VAR = model.ReactivePowerL2NetT0_VAR.ToFloat();
    entity.ReactivePowerL3NetT0_VAR = model.ReactivePowerL3NetT0_VAR.ToFloat();
    entity.ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh.ToFloat();
    entity.ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh.ToFloat();
    entity.ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh.ToFloat();
    entity.ActiveEnergyL1ExportT0_Wh = model.ActiveEnergyL1ExportT0_Wh.ToFloat();
    entity.ActiveEnergyL2ExportT0_Wh = model.ActiveEnergyL2ExportT0_Wh.ToFloat();
    entity.ActiveEnergyL3ExportT0_Wh = model.ActiveEnergyL3ExportT0_Wh.ToFloat();
    entity.ReactiveEnergyL1ImportT0_VARh = model.ReactiveEnergyL1ImportT0_VARh.ToFloat();
    entity.ReactiveEnergyL2ImportT0_VARh = model.ReactiveEnergyL2ImportT0_VARh.ToFloat();
    entity.ReactiveEnergyL3ImportT0_VARh = model.ReactiveEnergyL3ImportT0_VARh.ToFloat();
    entity.ReactiveEnergyL1ExportT0_VARh = model.ReactiveEnergyL1ExportT0_VARh.ToFloat();
    entity.ReactiveEnergyL2ExportT0_VARh = model.ReactiveEnergyL2ExportT0_VARh.ToFloat();
    entity.ReactiveEnergyL3ExportT0_VARh = model.ReactiveEnergyL3ExportT0_VARh.ToFloat();
    entity.ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh.ToFloat();
    entity.ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh.ToFloat();
    entity.ReactiveEnergyTotalImportT0_VARh = model.ReactiveEnergyTotalImportT0_VARh.ToFloat();
    entity.ReactiveEnergyTotalExportT0_VARh = model.ReactiveEnergyTotalExportT0_VARh.ToFloat();
    entity.ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh.ToFloat();
    entity.ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh.ToFloat();
  }

  public override void InitializeModel(
    AbbB2xMeasurementEntity entity,
    AbbB2xMeasurementModel model
  )
  {
    base.InitializeModel(entity, model);
    model.VoltageL1AnyT0_V = entity.VoltageL1AnyT0_V.ToDecimal();
    model.VoltageL2AnyT0_V = entity.VoltageL2AnyT0_V.ToDecimal();
    model.VoltageL3AnyT0_V = entity.VoltageL3AnyT0_V.ToDecimal();
    model.CurrentL1AnyT0_A = entity.CurrentL1AnyT0_A.ToDecimal();
    model.CurrentL2AnyT0_A = entity.CurrentL2AnyT0_A.ToDecimal();
    model.CurrentL3AnyT0_A = entity.CurrentL3AnyT0_A.ToDecimal();
    model.ActivePowerL1NetT0_W = entity.ActivePowerL1NetT0_W.ToDecimal();
    model.ActivePowerL2NetT0_W = entity.ActivePowerL2NetT0_W.ToDecimal();
    model.ActivePowerL3NetT0_W = entity.ActivePowerL3NetT0_W.ToDecimal();
    model.ReactivePowerL1NetT0_VAR = entity.ReactivePowerL1NetT0_VAR.ToDecimal();
    model.ReactivePowerL2NetT0_VAR = entity.ReactivePowerL2NetT0_VAR.ToDecimal();
    model.ReactivePowerL3NetT0_VAR = entity.ReactivePowerL3NetT0_VAR.ToDecimal();
    model.ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh.ToDecimal();
    model.ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh.ToDecimal();
    model.ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh.ToDecimal();
    model.ActiveEnergyL1ExportT0_Wh = entity.ActiveEnergyL1ExportT0_Wh.ToDecimal();
    model.ActiveEnergyL2ExportT0_Wh = entity.ActiveEnergyL2ExportT0_Wh.ToDecimal();
    model.ActiveEnergyL3ExportT0_Wh = entity.ActiveEnergyL3ExportT0_Wh.ToDecimal();
    model.ReactiveEnergyL1ImportT0_VARh = entity.ReactiveEnergyL1ImportT0_VARh.ToDecimal();
    model.ReactiveEnergyL2ImportT0_VARh = entity.ReactiveEnergyL2ImportT0_VARh.ToDecimal();
    model.ReactiveEnergyL3ImportT0_VARh = entity.ReactiveEnergyL3ImportT0_VARh.ToDecimal();
    model.ReactiveEnergyL1ExportT0_VARh = entity.ReactiveEnergyL1ExportT0_VARh.ToDecimal();
    model.ReactiveEnergyL2ExportT0_VARh = entity.ReactiveEnergyL2ExportT0_VARh.ToDecimal();
    model.ReactiveEnergyL3ExportT0_VARh = entity.ReactiveEnergyL3ExportT0_VARh.ToDecimal();
    model.ActiveEnergyTotalImportT0_Wh = entity.ActiveEnergyTotalImportT0_Wh.ToDecimal();
    model.ActiveEnergyTotalExportT0_Wh = entity.ActiveEnergyTotalExportT0_Wh.ToDecimal();
    model.ReactiveEnergyTotalImportT0_VARh = entity.ReactiveEnergyTotalImportT0_VARh.ToDecimal();
    model.ReactiveEnergyTotalExportT0_VARh = entity.ReactiveEnergyTotalExportT0_VARh.ToDecimal();
    model.ActiveEnergyTotalImportT1_Wh = entity.ActiveEnergyTotalImportT1_Wh.ToDecimal();
    model.ActiveEnergyTotalImportT2_Wh = entity.ActiveEnergyTotalImportT2_Wh.ToDecimal();
  }
}
