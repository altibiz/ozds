using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class FeeCalculationItemModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  FeeCalculationItemModel,
  CalculationItemModel,
  FeeCalculationItemEntity,
  CalculationItemEntity>(serviceProvider)
{
  public override void InitializeEntity(
    FeeCalculationItemModel model,
    FeeCalculationItemEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Amount_N = model.Amount_N;
  }
}
