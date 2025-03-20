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
    entity.Min = model.Min.ToFloat();
    entity.Max = model.Max.ToFloat();
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
    model.Min = entity.Min.ToDecimal();
    model.Max = entity.Max.ToDecimal();
    model.Avg = entity.Avg.ToDecimal();
    model.MinTimestamp = entity.MinTimestamp;
    model.MaxTimestamp = entity.MaxTimestamp;
  }
}
