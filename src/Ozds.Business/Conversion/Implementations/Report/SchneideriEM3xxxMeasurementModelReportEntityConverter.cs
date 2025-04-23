using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class SchneideriEM3xxxMeasurementModelReportEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelReportEntityConverter<
    SchneideriEM3xxxMeasurementModel,
    MeasurementModel,
    SchneideriEM3xxxMeasurementEntity,
    MeasurementEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    SchneideriEM3xxxMeasurementModel model,
    SchneideriEM3xxxMeasurementEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.VoltageL1AnyT0_V = model.VoltageL1AnyT0_V;
    entity.VoltageL2AnyT0_V = model.VoltageL2AnyT0_V;
    entity.VoltageL3AnyT0_V = model.VoltageL3AnyT0_V;
    entity.CurrentL1AnyT0_A = model.CurrentL1AnyT0_A;
    entity.CurrentL2AnyT0_A = model.CurrentL2AnyT0_A;
    entity.CurrentL3AnyT0_A = model.CurrentL3AnyT0_A;
    entity.ActivePowerL1NetT0_W = model.ActivePowerL1NetT0_W;
    entity.ActivePowerL2NetT0_W = model.ActivePowerL2NetT0_W;
    entity.ActivePowerL3NetT0_W = model.ActivePowerL3NetT0_W;
    entity.ReactivePowerTotalNetT0_VAR = model.ReactivePowerTotalNetT0_VAR;
    entity.ApparentPowerTotalNetT0_VA = model.ApparentPowerTotalNetT0_VA;
    entity.ActiveEnergyL1ImportT0_Wh = model.ActiveEnergyL1ImportT0_Wh;
    entity.ActiveEnergyL2ImportT0_Wh = model.ActiveEnergyL2ImportT0_Wh;
    entity.ActiveEnergyL3ImportT0_Wh = model.ActiveEnergyL3ImportT0_Wh;
    entity.ActiveEnergyTotalImportT0_Wh = model.ActiveEnergyTotalImportT0_Wh;
    entity.ActiveEnergyTotalExportT0_Wh = model.ActiveEnergyTotalExportT0_Wh;
    entity.ReactiveEnergyTotalImportT0_VARh =
      model.ReactiveEnergyTotalImportT0_VARh;
    entity.ReactiveEnergyTotalExportT0_VARh =
      model.ReactiveEnergyTotalExportT0_VARh;
    entity.ActiveEnergyTotalImportT1_Wh = model.ActiveEnergyTotalImportT1_Wh;
    entity.ActiveEnergyTotalImportT2_Wh = model.ActiveEnergyTotalImportT2_Wh;
  }

  public override void InitializeModel(
    SchneideriEM3xxxMeasurementEntity entity,
    SchneideriEM3xxxMeasurementModel model
  )
  {
    base.InitializeModel(entity, model);
    model.VoltageL1AnyT0_V = entity.VoltageL1AnyT0_V;
    model.VoltageL2AnyT0_V = entity.VoltageL2AnyT0_V;
    model.VoltageL3AnyT0_V = entity.VoltageL3AnyT0_V;
    model.CurrentL1AnyT0_A = entity.CurrentL1AnyT0_A;
    model.CurrentL2AnyT0_A = entity.CurrentL2AnyT0_A;
    model.CurrentL3AnyT0_A = entity.CurrentL3AnyT0_A;
    model.ActivePowerL1NetT0_W = entity.ActivePowerL1NetT0_W;
    model.ActivePowerL2NetT0_W = entity.ActivePowerL2NetT0_W;
    model.ActivePowerL3NetT0_W = entity.ActivePowerL3NetT0_W;
    model.ReactivePowerTotalNetT0_VAR = entity.ReactivePowerTotalNetT0_VAR;
    model.ApparentPowerTotalNetT0_VA = entity.ApparentPowerTotalNetT0_VA;
    model.ActiveEnergyL1ImportT0_Wh = entity.ActiveEnergyL1ImportT0_Wh;
    model.ActiveEnergyL2ImportT0_Wh = entity.ActiveEnergyL2ImportT0_Wh;
    model.ActiveEnergyL3ImportT0_Wh = entity.ActiveEnergyL3ImportT0_Wh;
    model.ActiveEnergyTotalImportT0_Wh = entity.ActiveEnergyTotalImportT0_Wh;
    model.ActiveEnergyTotalExportT0_Wh = entity.ActiveEnergyTotalExportT0_Wh;
    model.ReactiveEnergyTotalImportT0_VARh =
      entity.ReactiveEnergyTotalImportT0_VARh;
    model.ReactiveEnergyTotalExportT0_VARh =
      entity.ReactiveEnergyTotalExportT0_VARh;
    model.ActiveEnergyTotalImportT1_Wh = entity.ActiveEnergyTotalImportT1_Wh;
    model.ActiveEnergyTotalImportT2_Wh = entity.ActiveEnergyTotalImportT2_Wh;
  }
}
