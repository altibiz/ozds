using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class SchneideriEM3xxxAggregateModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      SchneideriEM3xxxAggregateModel,
      AggregateModel,
      SchneideriEM3xxxAggregateEntity,
      AggregateEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    SchneideriEM3xxxAggregateModel model,
    SchneideriEM3xxxAggregateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.VoltageL1AnyT0_V =
      model.VoltageL1AnyT0_V is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.VoltageL1AnyT0_V);
    entity.VoltageL2AnyT0_V =
      model.VoltageL2AnyT0_V is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.VoltageL2AnyT0_V);
    entity.VoltageL3AnyT0_V =
      model.VoltageL3AnyT0_V is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.VoltageL3AnyT0_V);
    entity.CurrentL1AnyT0_A =
      model.CurrentL1AnyT0_A is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.CurrentL1AnyT0_A);
    entity.CurrentL2AnyT0_A =
      model.CurrentL2AnyT0_A is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.CurrentL2AnyT0_A);
    entity.CurrentL3AnyT0_A =
      model.CurrentL3AnyT0_A is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.CurrentL3AnyT0_A);
    entity.ActivePowerL1NetT0_W =
      model.ActivePowerL1NetT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.ActivePowerL1NetT0_W);
    entity.ActivePowerL2NetT0_W =
      model.ActivePowerL2NetT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.ActivePowerL2NetT0_W);
    entity.ActivePowerL3NetT0_W =
      model.ActivePowerL3NetT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.ActivePowerL3NetT0_W);
    entity.ReactivePowerTotalNetT0_VAR =
      model.ReactivePowerTotalNetT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.ReactivePowerTotalNetT0_VAR);
    entity.ApparentPowerTotalNetT0_VA =
      model.ApparentPowerTotalNetT0_VA is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.ApparentPowerTotalNetT0_VA);
    entity.ActiveEnergyL1ImportT0_Wh =
      model.ActiveEnergyL1ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyL1ImportT0_Wh);
    entity.DerivedActivePowerL1ImportT0_W =
      model.DerivedActivePowerL1ImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerL1ImportT0_W);
    entity.ActiveEnergyL2ImportT0_Wh =
      model.ActiveEnergyL2ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyL2ImportT0_Wh);
    entity.DerivedActivePowerL2ImportT0_W =
      model.DerivedActivePowerL2ImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerL2ImportT0_W);
    entity.ActiveEnergyL3ImportT0_Wh =
      model.ActiveEnergyL3ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyL3ImportT0_Wh);
    entity.DerivedActivePowerL3ImportT0_W =
      model.DerivedActivePowerL3ImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerL3ImportT0_W);
    entity.ActiveEnergyTotalImportT0_Wh =
      model.ActiveEnergyTotalImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyTotalImportT0_Wh);
    entity.DerivedActivePowerTotalImportT0_W =
      model.DerivedActivePowerTotalImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerTotalImportT0_W);
    entity.ActiveEnergyTotalExportT0_Wh =
      model.ActiveEnergyTotalExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyTotalExportT0_Wh);
    entity.DerivedActivePowerTotalExportT0_W =
      model.DerivedActivePowerTotalExportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerTotalExportT0_W);
    entity.ReactiveEnergyTotalImportT0_VARh =
      model.ReactiveEnergyTotalImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ReactiveEnergyTotalImportT0_VARh);
    entity.DerivedReactivePowerTotalImportT0_VAR =
      model.DerivedReactivePowerTotalImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedReactivePowerTotalImportT0_VAR);
    entity.ReactiveEnergyTotalExportT0_VARh =
      model.ReactiveEnergyTotalExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ReactiveEnergyTotalExportT0_VARh);
    entity.DerivedReactivePowerTotalExportT0_VAR =
      model.DerivedReactivePowerTotalExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedReactivePowerTotalExportT0_VAR);
    entity.ActiveEnergyTotalImportT1_Wh =
      model.ActiveEnergyTotalImportT1_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyTotalImportT1_Wh);
    entity.DerivedActivePowerTotalImportT1_W =
      model.DerivedActivePowerTotalImportT1_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerTotalImportT1_W);
    entity.ActiveEnergyTotalImportT2_Wh =
      model.ActiveEnergyTotalImportT2_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<CumulativeAggregateMeasureEntity>(
            model.ActiveEnergyTotalImportT2_Wh);
    entity.DerivedActivePowerTotalImportT2_W =
      model.DerivedActivePowerTotalImportT2_W is null
        ? null!
        : modelEntityConverter.ToEntity<InstantaneousAggregateMeasureEntity>(
            model.DerivedActivePowerTotalImportT2_W);
  }

  public override void InitializeModel(
    SchneideriEM3xxxAggregateEntity entity,
    SchneideriEM3xxxAggregateModel model
  )
  {
    base.InitializeModel(entity, model);
    model.VoltageL1AnyT0_V =
      entity.VoltageL1AnyT0_V is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.VoltageL1AnyT0_V);
    model.VoltageL2AnyT0_V =
      entity.VoltageL2AnyT0_V is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.VoltageL2AnyT0_V);
    model.VoltageL3AnyT0_V =
      entity.VoltageL3AnyT0_V is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.VoltageL3AnyT0_V);
    model.CurrentL1AnyT0_A =
      entity.CurrentL1AnyT0_A is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.CurrentL1AnyT0_A);
    model.CurrentL2AnyT0_A =
      entity.CurrentL2AnyT0_A is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.CurrentL2AnyT0_A);
    model.CurrentL3AnyT0_A =
      entity.CurrentL3AnyT0_A is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.CurrentL3AnyT0_A);
    model.ActivePowerL1NetT0_W =
      entity.ActivePowerL1NetT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ActivePowerL1NetT0_W);
    model.ActivePowerL2NetT0_W =
      entity.ActivePowerL2NetT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ActivePowerL2NetT0_W);
    model.ActivePowerL3NetT0_W =
      entity.ActivePowerL3NetT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ActivePowerL3NetT0_W);
    model.ReactivePowerTotalNetT0_VAR =
      entity.ReactivePowerTotalNetT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ReactivePowerTotalNetT0_VAR);
    model.ApparentPowerTotalNetT0_VA =
      entity.ApparentPowerTotalNetT0_VA is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ApparentPowerTotalNetT0_VA);
    model.ActiveEnergyL1ImportT0_Wh =
      entity.ActiveEnergyL1ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyL1ImportT0_Wh);
    model.DerivedActivePowerL1ImportT0_W =
      entity.DerivedActivePowerL1ImportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerL1ImportT0_W);
    model.ActiveEnergyL2ImportT0_Wh =
      entity.ActiveEnergyL2ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyL2ImportT0_Wh);
    model.DerivedActivePowerL2ImportT0_W =
      entity.DerivedActivePowerL2ImportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerL2ImportT0_W);
    model.ActiveEnergyL3ImportT0_Wh =
      entity.ActiveEnergyL3ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyL3ImportT0_Wh);
    model.DerivedActivePowerL3ImportT0_W =
      entity.DerivedActivePowerL3ImportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerL3ImportT0_W);
    model.ActiveEnergyTotalImportT0_Wh =
      entity.ActiveEnergyTotalImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyTotalImportT0_Wh);
    model.DerivedActivePowerTotalImportT0_W =
      entity.DerivedActivePowerTotalImportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerTotalImportT0_W);
    model.ActiveEnergyTotalExportT0_Wh =
      entity.ActiveEnergyTotalExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyTotalExportT0_Wh);
    model.DerivedActivePowerTotalExportT0_W =
      entity.DerivedActivePowerTotalExportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerTotalExportT0_W);
    model.ReactiveEnergyTotalImportT0_VARh =
      entity.ReactiveEnergyTotalImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyTotalImportT0_VARh);
    model.DerivedReactivePowerTotalImportT0_VAR =
      entity.DerivedReactivePowerTotalImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerTotalImportT0_VAR);
    model.ReactiveEnergyTotalExportT0_VARh =
      entity.ReactiveEnergyTotalExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyTotalExportT0_VARh);
    model.DerivedReactivePowerTotalExportT0_VAR =
      entity.DerivedReactivePowerTotalExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerTotalExportT0_VAR);
    model.ActiveEnergyTotalImportT1_Wh =
      entity.ActiveEnergyTotalImportT1_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyTotalImportT1_Wh);
    model.DerivedActivePowerTotalImportT1_W =
      entity.DerivedActivePowerTotalImportT1_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerTotalImportT1_W);
    model.ActiveEnergyTotalImportT2_Wh =
      entity.ActiveEnergyTotalImportT2_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyTotalImportT2_Wh);
    model.DerivedActivePowerTotalImportT2_W =
      entity.DerivedActivePowerTotalImportT2_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerTotalImportT2_W);
  }
}
