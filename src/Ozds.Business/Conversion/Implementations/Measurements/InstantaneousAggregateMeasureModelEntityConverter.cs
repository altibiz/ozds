using Ozds.Business.Conversion.Base;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class InstantaneousAggregateMeasureEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      InstantaneousAggregateMeasureModel,
      AggregateMeasureModel,
      InstantaneousAggregateMeasureEntity,
      AggregateMeasureEntity>(serviceProvider)
{
  public override void InitializeEntity(
    InstantaneousAggregateMeasureModel model,
    InstantaneousAggregateMeasureEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Avg = model.Avg.ToFloat();
    entity.MinTimestamp = model.MinTimestamp;
    entity.MaxTimestamp = model.MaxTimestamp;
  }

  public override void InitializeModel(
    InstantaneousAggregateMeasureEntity entity,
    InstantaneousAggregateMeasureModel model
  )
  {
    base.InitializeModel(entity, model);
    model.Avg = entity.Avg.ToDecimal();
    model.MinTimestamp = entity.MinTimestamp;
    model.MaxTimestamp = entity.MaxTimestamp;
  }
}
