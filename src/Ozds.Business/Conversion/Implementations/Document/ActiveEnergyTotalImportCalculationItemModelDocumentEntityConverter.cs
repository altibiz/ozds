using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class ActiveEnergyTotalImportCalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  ActiveEnergyTotalImportCalculationItemModel,
  CalculationItemModel,
  ActiveEnergyTotalImportCalculationItemEntity,
  CalculationItemEntity>(serviceProvider)
{
  public override void InitializeEntity(
    ActiveEnergyTotalImportCalculationItemModel model,
    ActiveEnergyTotalImportCalculationItemEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Min_kWh = model.Min_kWh;
    entity.Max_kWh = model.Max_kWh;
    entity.Amount_kWh = model.Amount_kWh;
  }
}
