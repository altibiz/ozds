using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class ActivePowerTotalImportT1PeakCalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
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
}

public class
  UsageActivePowerTotalImportT1PeakCalculationItemModelDocumentEntityConverter(
    IServiceProvider serviceProvider
  ) : InheritingModelDocumentEntityConverter<
  UsageActivePowerTotalImportT1PeakCalculationItemModel,
  ActivePowerTotalImportT1PeakCalculationItemModel,
  UsageActivePowerTotalImportT1PeakCalculationItemEntity,
  ActivePowerTotalImportT1PeakCalculationItemEntity>(serviceProvider)
{
}
