using Ozds.Business.Conversion.Base;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class CumulativeAggregateMeasureEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelEntityConverter<
    CumulativeAggregateMeasureModel,
    AggregateMeasureModel,
    CumulativeAggregateMeasureEntity,
    AggregateMeasureEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    CumulativeAggregateMeasureModel model,
    CumulativeAggregateMeasureEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Min = model.Min.ToLong();
    entity.Max = model.Max.ToLong();
  }

  public override void InitializeModel(
    CumulativeAggregateMeasureEntity entity,
    CumulativeAggregateMeasureModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Min = entity.Min.ToDecimal();
    model.Max = entity.Max.ToDecimal();
  }
}
