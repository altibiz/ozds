using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Primitive;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AggregateMeasureModelEntityConverter
  : ConcreteModelEntityConverter<
      AggregateMeasureModel,
      AggregateMeasureEntity>
{
  public override void InitializeEntity(
    AggregateMeasureModel model,
    AggregateMeasureEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Min = model.Min.ToFloat();
    entity.Max = model.Max.ToFloat();
  }

  public override void InitializeModel(
    AggregateMeasureEntity entity,
    AggregateMeasureModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Min = entity.Min.ToDecimal();
    model.Max = entity.Max.ToDecimal();
  }
}
