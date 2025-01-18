using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AbbB2xAggregateModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      AbbB2xAggregateModel,
      AggregateModel,
      AbbB2xAggregateEntity,
      AggregateEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    AbbB2xAggregateModel model,
    AbbB2xAggregateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.VoltageL1AnyT0_V =
      model.VoltageL1AnyT0_V is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.VoltageL1AnyT0_V);
    entity.VoltageL2AnyT0_V =
      model.VoltageL2AnyT0_V is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.VoltageL2AnyT0_V);
    entity.VoltageL3AnyT0_V =
      model.VoltageL3AnyT0_V is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.VoltageL3AnyT0_V);
    entity.CurrentL1AnyT0_A =
      model.CurrentL1AnyT0_A is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.CurrentL1AnyT0_A);
    entity.CurrentL2AnyT0_A =
      model.CurrentL2AnyT0_A is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.CurrentL2AnyT0_A);
    entity.CurrentL3AnyT0_A =
      model.CurrentL3AnyT0_A is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.CurrentL3AnyT0_A);
    entity.ActivePowerL1NetT0_W =
      model.ActivePowerL1NetT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.ActivePowerL1NetT0_W);
    entity.ActivePowerL2NetT0_W =
      model.ActivePowerL2NetT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.ActivePowerL2NetT0_W);
    entity.ActivePowerL3NetT0_W =
      model.ActivePowerL3NetT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.ActivePowerL3NetT0_W);
    entity.ReactivePowerL1NetT0_VAR =
      model.ReactivePowerL1NetT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.ReactivePowerL1NetT0_VAR);
    entity.ReactivePowerL2NetT0_VAR =
      model.ReactivePowerL2NetT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.ReactivePowerL2NetT0_VAR);
    entity.ReactivePowerL3NetT0_VAR =
      model.ReactivePowerL3NetT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.ReactivePowerL3NetT0_VAR);
    entity.ActiveEnergyL1ImportT0_Wh =
      model.ActiveEnergyL1ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyL1ImportT0_Wh);
    entity.DerivedActivePowerL1ImportT0_W =
      model.DerivedActivePowerL1ImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerL1ImportT0_W);
    entity.ActiveEnergyL2ImportT0_Wh =
      model.ActiveEnergyL2ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyL2ImportT0_Wh);
    entity.DerivedActivePowerL2ImportT0_W =
      model.DerivedActivePowerL2ImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerL2ImportT0_W);
    entity.ActiveEnergyL3ImportT0_Wh =
      model.ActiveEnergyL3ImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyL3ImportT0_Wh);
    entity.DerivedActivePowerL3ImportT0_W =
      model.DerivedActivePowerL3ImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerL3ImportT0_W);
    entity.ActiveEnergyTotalImportT0_Wh =
      model.ActiveEnergyTotalImportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyTotalImportT0_Wh);
    entity.DerivedActivePowerTotalImportT0_W =
      model.DerivedActivePowerTotalImportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerTotalImportT0_W);
    entity.ActiveEnergyL1ExportT0_Wh =
      model.ActiveEnergyL1ExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyL1ExportT0_Wh);
    entity.DerivedActivePowerL1ExportT0_W =
      model.DerivedActivePowerL1ExportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerL1ExportT0_W);
    entity.ActiveEnergyL2ExportT0_Wh =
      model.ActiveEnergyL2ExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyL2ExportT0_Wh);
    entity.DerivedActivePowerL2ExportT0_W =
      model.DerivedActivePowerL2ExportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerL2ExportT0_W);
    entity.ActiveEnergyL3ExportT0_Wh =
      model.ActiveEnergyL3ExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyL3ExportT0_Wh);
    entity.DerivedActivePowerL3ExportT0_W =
      model.DerivedActivePowerL3ExportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerL3ExportT0_W);
    entity.ActiveEnergyTotalExportT0_Wh =
      model.ActiveEnergyTotalExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyTotalExportT0_Wh);
    entity.DerivedActivePowerTotalExportT0_W =
      model.DerivedActivePowerTotalExportT0_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerTotalExportT0_W);
    entity.ActiveEnergyTotalImportT1_Wh =
      model.ActiveEnergyTotalImportT1_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyTotalImportT1_Wh);
    entity.DerivedActivePowerTotalImportT1_W =
      model.DerivedActivePowerTotalImportT1_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerTotalImportT1_W);
    entity.ActiveEnergyTotalImportT2_Wh =
      model.ActiveEnergyTotalImportT2_Wh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ActiveEnergyTotalImportT2_Wh);
    entity.DerivedActivePowerTotalImportT2_W =
      model.DerivedActivePowerTotalImportT2_W is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedActivePowerTotalImportT2_W);
    entity.ReactiveEnergyL1ImportT0_VARh =
      model.ReactiveEnergyL1ImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyL1ImportT0_VARh);
    entity.DerivedReactivePowerL1ImportT0_VAR =
      model.DerivedReactivePowerL1ImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerL1ImportT0_VAR);
    entity.ReactiveEnergyL2ImportT0_VARh =
      model.ReactiveEnergyL2ImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyL2ImportT0_VARh);
    entity.DerivedReactivePowerL2ImportT0_VAR =
      model.DerivedReactivePowerL2ImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerL2ImportT0_VAR);
    entity.ReactiveEnergyL3ImportT0_VARh =
      model.ReactiveEnergyL3ImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyL3ImportT0_VARh);
    entity.DerivedReactivePowerL3ImportT0_VAR =
      model.DerivedReactivePowerL3ImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerL3ImportT0_VAR);
    entity.ReactiveEnergyTotalImportT0_VARh =
      model.ReactiveEnergyTotalImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyTotalImportT0_VARh);
    entity.DerivedReactivePowerTotalImportT0_VAR =
      model.DerivedReactivePowerTotalImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerTotalImportT0_VAR);
    entity.ReactiveEnergyL1ExportT0_VARh =
      model.ReactiveEnergyL1ExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyL1ExportT0_VARh);
    entity.DerivedReactivePowerL1ExportT0_VAR =
      model.DerivedReactivePowerL1ExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerL1ExportT0_VAR);
    entity.ReactiveEnergyL2ExportT0_VARh =
      model.ReactiveEnergyL2ExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyL2ExportT0_VARh);
    entity.DerivedReactivePowerL2ExportT0_VAR =
      model.DerivedReactivePowerL2ExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerL2ExportT0_VAR);
    entity.ReactiveEnergyL3ExportT0_VARh =
      model.ReactiveEnergyL3ExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyL3ExportT0_VARh);
    entity.DerivedReactivePowerL3ExportT0_VAR =
      model.DerivedReactivePowerL3ExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerL3ExportT0_VAR);
    entity.ReactiveEnergyTotalExportT0_VARh =
      model.ReactiveEnergyTotalExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToEntity<
            CumulativeAggregateMeasureEntity>(
              model.ReactiveEnergyTotalExportT0_VARh);
    entity.DerivedReactivePowerTotalExportT0_VAR =
      model.DerivedReactivePowerTotalExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToEntity<
            InstantaneousAggregateMeasureEntity>(
              model.DerivedReactivePowerTotalExportT0_VAR);
  }

  public override void InitializeModel(
    AbbB2xAggregateEntity entity,
    AbbB2xAggregateModel model
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
    model.ReactivePowerL1NetT0_VAR =
      entity.ReactivePowerL1NetT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ReactivePowerL1NetT0_VAR);
    model.ReactivePowerL2NetT0_VAR =
      entity.ReactivePowerL2NetT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ReactivePowerL2NetT0_VAR);
    model.ReactivePowerL3NetT0_VAR =
      entity.ReactivePowerL3NetT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.ReactivePowerL3NetT0_VAR);
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
    model.ActiveEnergyL1ExportT0_Wh =
      entity.ActiveEnergyL1ExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyL1ExportT0_Wh);
    model.DerivedActivePowerL1ExportT0_W =
      entity.DerivedActivePowerL1ExportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerL1ExportT0_W);
    model.ActiveEnergyL2ExportT0_Wh =
      entity.ActiveEnergyL2ExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyL2ExportT0_Wh);
    model.DerivedActivePowerL2ExportT0_W =
      entity.DerivedActivePowerL2ExportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerL2ExportT0_W);
    model.ActiveEnergyL3ExportT0_Wh =
      entity.ActiveEnergyL3ExportT0_Wh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ActiveEnergyL3ExportT0_Wh);
    model.DerivedActivePowerL3ExportT0_W =
      entity.DerivedActivePowerL3ExportT0_W is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedActivePowerL3ExportT0_W);
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
    model.ReactiveEnergyL1ImportT0_VARh =
      entity.ReactiveEnergyL1ImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyL1ImportT0_VARh);
    model.DerivedReactivePowerL1ImportT0_VAR =
      entity.DerivedReactivePowerL1ImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerL1ImportT0_VAR);
    model.ReactiveEnergyL2ImportT0_VARh =
      entity.ReactiveEnergyL2ImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyL2ImportT0_VARh);
    model.DerivedReactivePowerL2ImportT0_VAR =
      entity.DerivedReactivePowerL2ImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerL2ImportT0_VAR);
    model.ReactiveEnergyL3ImportT0_VARh =
      entity.ReactiveEnergyL3ImportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyL3ImportT0_VARh);
    model.DerivedReactivePowerL3ImportT0_VAR =
      entity.DerivedReactivePowerL3ImportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerL3ImportT0_VAR);
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
    model.ReactiveEnergyL1ExportT0_VARh =
      entity.ReactiveEnergyL1ExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyL1ExportT0_VARh);
    model.DerivedReactivePowerL1ExportT0_VAR =
      entity.DerivedReactivePowerL1ExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerL1ExportT0_VAR);
    model.ReactiveEnergyL2ExportT0_VARh =
      entity.ReactiveEnergyL2ExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyL2ExportT0_VARh);
    model.DerivedReactivePowerL2ExportT0_VAR =
      entity.DerivedReactivePowerL2ExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerL2ExportT0_VAR);
    model.ReactiveEnergyL3ExportT0_VARh =
      entity.ReactiveEnergyL3ExportT0_VARh is null
        ? null!
        : modelEntityConverter.ToModel<CumulativeAggregateMeasureModel>(
            entity.ReactiveEnergyL3ExportT0_VARh);
    model.DerivedReactivePowerL3ExportT0_VAR =
      entity.DerivedReactivePowerL3ExportT0_VAR is null
        ? null!
        : modelEntityConverter.ToModel<InstantaneousAggregateMeasureModel>(
            entity.DerivedReactivePowerL3ExportT0_VAR);
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
  }
}
