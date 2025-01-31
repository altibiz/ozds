using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class MessengerEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    MessengerModel,
    AuditableModel,
    MessengerEntity,
    AuditableEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    MessengerModel model,
    MessengerEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.LocationId = model.LocationId;
    entity.MaxInactivityPeriod = modelEntityConverter.ToEntity<PeriodEntity>(
      model.MaxInactivityPeriod);
    entity.PushDelayPeriod = modelEntityConverter.ToEntity<PeriodEntity>(
      model.PushDelayPeriod);
  }

  public override void InitializeModel(
    MessengerEntity entity,
    MessengerModel model
  )
  {
    base.InitializeModel(entity, model);
    model.LocationId = entity.LocationId;
    model.MaxInactivityPeriod = modelEntityConverter.ToModel<PeriodModel>(
      entity.MaxInactivityPeriod);
    model.PushDelayPeriod = modelEntityConverter.ToModel<PeriodModel>(
      entity.PushDelayPeriod);
  }
}
