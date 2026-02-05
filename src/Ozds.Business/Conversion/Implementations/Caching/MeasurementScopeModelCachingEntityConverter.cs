using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class MeasurementScopeModelCachingEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelCachingEntityConverter<
    MeasurementScopeModel,
    ScopeModel,
    MeasurementScopeEntity,
    ScopeEntity
  >(serviceProvider)
{
  private readonly ModelCachingEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelCachingEntityConverter>();

  public override void InitializeEntity(
    MeasurementScopeModel model,
    MeasurementScopeEntity entity
  )
  {
    base.InitializeEntity(model, entity);

    entity.Interval = modelEntityConverter.ToEntity<IntervalEntity>(
      model.Interval
    );
  }

  public override void InitializeModel(
    MeasurementScopeEntity entity,
    MeasurementScopeModel model
  )
  {
    base.InitializeModel(entity, model);

    model.Interval = modelEntityConverter.ToModel<IntervalModel>(
      entity.Interval
    );
  }
}
