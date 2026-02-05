using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class MessengerModelCachingEntityConverter(
  IServiceProvider serviceProvider
)
  : InheritingModelCachingEntityConverter<
    MessengerModel,
    TrackableModel,
    MessengerEntity,
    TrackableEntity
  >(serviceProvider)
{
  private readonly ModelCachingEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelCachingEntityConverter>();

  public override void InitializeEntity(
    MessengerModel model,
    MessengerEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.LocationId = model.LocationId;
    entity.MaxInactivityPeriod = modelEntityConverter.ToEntity<PeriodEntity>(
      model.MaxInactivityPeriod
    );
    entity.PushDelayPeriod = modelEntityConverter.ToEntity<PeriodEntity>(
      model.PushDelayPeriod
    );
  }

  public override void InitializeModel(
    MessengerEntity entity,
    MessengerModel model
  )
  {
    base.InitializeModel(entity, model);
    model.LocationId = entity.LocationId;
    model.MaxInactivityPeriod = modelEntityConverter.ToModel<PeriodModel>(
      entity.MaxInactivityPeriod
    );
    model.PushDelayPeriod = modelEntityConverter.ToModel<PeriodModel>(
      entity.PushDelayPeriod
    );
  }
}
