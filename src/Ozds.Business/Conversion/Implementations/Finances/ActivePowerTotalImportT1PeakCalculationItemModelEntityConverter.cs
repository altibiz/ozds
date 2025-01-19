using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class ActivePowerTotalImportT1PeakCalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelEntityConverter<
      ActivePowerTotalImportT1PeakCalculationItemModel,
      CalculationItemModel,
      ActivePowerTotalImportT1PeakCalculationItemEntity,
      CalculationItemEntity>(serviceProvider)
{
  public override void InitializeEntity(
    ActivePowerTotalImportT1PeakCalculationItemModel model,
    ActivePowerTotalImportT1PeakCalculationItemEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Peak_kW = model.Peak_kW;
    entity.Amount_kW = model.Amount_kW;
  }

  public override void InitializeModel(
    ActivePowerTotalImportT1PeakCalculationItemEntity entity,
    ActivePowerTotalImportT1PeakCalculationItemModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Peak_kW = entity.Peak_kW;
    model.Amount_kW = entity.Amount_kW;
  }
}

public class
  UsageActivePowerTotalImportT1PeakCalculationItemModelEntityConverter(
    IServiceProvider serviceProvider
  ) : InheritingModelEntityConverter<
        UsageActivePowerTotalImportT1PeakCalculationItemModel,
        ActivePowerTotalImportT1PeakCalculationItemModel,
        UsageActivePowerTotalImportT1PeakCalculationItemEntity,
        ActivePowerTotalImportT1PeakCalculationItemEntity>(serviceProvider)
{
}
