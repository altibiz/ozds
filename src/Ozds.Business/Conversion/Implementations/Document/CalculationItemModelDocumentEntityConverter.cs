using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class CalculationItemModelDocumentEntityConverter
  : ConcreteModelDocumentEntityConverter<
    CalculationItemModel,
    CalculationItemEntity>
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
}
