using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CalculationItemModelEntityConverter
  : ConcreteModelEntityConverter<CalculationItemModel, CalculationItemEntity>
{
  public override void InitializeEntity(
    CalculationItemModel model,
    CalculationItemEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Price_EUR = model.Price_EUR;
    entity.Total_EUR = model.Total_EUR;
  }

  public override void InitializeModel(
    CalculationItemEntity entity,
    CalculationItemModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Price_EUR = entity.Price_EUR;
    model.Total_EUR = entity.Total_EUR;
  }
}
