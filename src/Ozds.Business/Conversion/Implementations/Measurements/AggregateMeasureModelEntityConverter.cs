using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class AggregateMeasureModelEntityConverter
  : ConcreteModelEntityConverter<
    AggregateMeasureModel,
    AggregateMeasureEntity>
{
#pragma warning disable S1185 // Overriding members should do more than simply call the same member in the base class
  public override void InitializeEntity(
    AggregateMeasureModel model,
    AggregateMeasureEntity entity
  )
  {
    base.InitializeEntity(model, entity);
  }

  public override void InitializeModel(
    AggregateMeasureEntity entity,
    AggregateMeasureModel model
  )
  {
    base.InitializeModel(entity, model);
  }
#pragma warning restore S1185 // Overriding members should do more than simply call the same member in the base class
}
