using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class MessengerNotificationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      MessengerNotificationModel,
      ResolvableNotificationModel,
      MessengerNotificationEntity,
      ResolvableNotificationEntity>(serviceProvider)
{
  public override void InitializeEntity(
    MessengerNotificationModel model,
    MessengerNotificationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.MessengerId = model.MessengerId;
  }

  public override void InitializeModel(
    MessengerNotificationEntity entity,
    MessengerNotificationModel model)
  {
    base.InitializeModel(entity, model);
    model.MessengerId = entity.MessengerId;
  }
}
