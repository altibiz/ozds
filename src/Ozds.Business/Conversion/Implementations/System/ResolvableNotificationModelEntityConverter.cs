using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class ResolvableNotificationEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  ResolvableNotificationModel,
  NotificationModel,
  ResolvableNotificationEntity,
  NotificationEntity>(serviceProvider)
{
  public override void InitializeEntity(
    ResolvableNotificationModel model,
    ResolvableNotificationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.ResolvedById = model.ResolvedById;
    entity.ResolvedOn = model.ResolvedOn;
  }

  public override void InitializeModel(
    ResolvableNotificationEntity entity,
    ResolvableNotificationModel model)
  {
    base.InitializeModel(entity, model);
    model.ResolvedById = entity.ResolvedById;
    model.ResolvedOn = entity.ResolvedOn;
  }
}
