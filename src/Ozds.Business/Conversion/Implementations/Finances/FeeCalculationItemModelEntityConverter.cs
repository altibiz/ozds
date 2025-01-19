using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class FeeCalculationItemModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
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

  public override void InitializeModel(
    FeeCalculationItemEntity entity,
    FeeCalculationItemModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Amount_N = entity.Amount_N;
  }
}
