using Ozds.Business.Conversion.Base;
using Ozds.Business.Extensions;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class DerivedAggregateMeasureEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  DerivedAggregateMeasureModel,
  AggregateMeasureModel,
  DerivedAggregateMeasureEntity,
  AggregateMeasureEntity>(serviceProvider)
{
  public override void InitializeEntity(
    DerivedAggregateMeasureModel model,
    DerivedAggregateMeasureEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Min = model.Min.ToLong();
    entity.Max = model.Max.ToLong();
    entity.Avg = model.Avg.ToDouble();
    entity.MinTimestamp = model.MinTimestamp;
    entity.MaxTimestamp = model.MaxTimestamp;
  }

  public override void InitializeModel(
    DerivedAggregateMeasureEntity entity,
    DerivedAggregateMeasureModel model
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
