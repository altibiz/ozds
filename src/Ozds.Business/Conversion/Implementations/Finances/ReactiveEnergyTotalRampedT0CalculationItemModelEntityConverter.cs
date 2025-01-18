using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class ReactiveEnergyTotalRampedT0CalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      ReactiveEnergyTotalRampedT0CalculationItemModel,
      CalculationItemModel,
      ReactiveEnergyTotalRampedT0CalculationItemEntity,
      CalculationItemEntity>(serviceProvider)
{
  public override void InitializeEntity(
    ReactiveEnergyTotalRampedT0CalculationItemModel model,
    ReactiveEnergyTotalRampedT0CalculationItemEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.ReactiveImportMin_kVARh = model.ReactiveImportMin_kVARh;
    entity.ReactiveImportMax_kVARh = model.ReactiveImportMax_kVARh;
    entity.ReactiveImportAmount_kVARh = model.ReactiveImportAmount_kVARh;
    entity.ReactiveExportMin_kVARh = model.ReactiveExportMin_kVARh;
    entity.ReactiveExportMax_kVARh = model.ReactiveExportMax_kVARh;
    entity.ReactiveExportAmount_kVARh = model.ReactiveExportAmount_kVARh;
    entity.ActiveImportMin_kWh = model.ActiveImportMin_kWh;
    entity.ActiveImportMax_kWh = model.ActiveImportMax_kWh;
    entity.ActiveImportAmount_kWh = model.ActiveImportAmount_kWh;
    entity.Amount_kVARh = model.Amount_kVARh;
  }

  public override void InitializeModel(
    ReactiveEnergyTotalRampedT0CalculationItemEntity entity,
    ReactiveEnergyTotalRampedT0CalculationItemModel model
  )
  {
    base.InitializeModel(entity, model);
    model.ReactiveImportMin_kVARh = entity.ReactiveImportMin_kVARh;
    model.ReactiveImportMax_kVARh = entity.ReactiveImportMax_kVARh;
    model.ReactiveImportAmount_kVARh = entity.ReactiveImportAmount_kVARh;
    model.ReactiveExportMin_kVARh = entity.ReactiveExportMin_kVARh;
    model.ReactiveExportMax_kVARh = entity.ReactiveExportMax_kVARh;
    model.ReactiveExportAmount_kVARh = entity.ReactiveExportAmount_kVARh;
    model.ActiveImportMin_kWh = entity.ActiveImportMin_kWh;
    model.ActiveImportMax_kWh = entity.ActiveImportMax_kWh;
    model.ActiveImportAmount_kWh = entity.ActiveImportAmount_kWh;
    model.Amount_kVARh = entity.Amount_kVARh;
  }
}

public class
  UsageReactiveEnergyTotalRampedT0CalculationItemModelEntityConverter(
    IServiceProvider serviceProvider
  ) : InheritingModelEntityConverter<
        UsageReactiveEnergyTotalRampedT0CalculationItemModel,
        ReactiveEnergyTotalRampedT0CalculationItemModel,
        UsageReactiveEnergyTotalRampedT0CalculationItemEntity,
        ReactiveEnergyTotalRampedT0CalculationItemEntity>(serviceProvider)
{
}
