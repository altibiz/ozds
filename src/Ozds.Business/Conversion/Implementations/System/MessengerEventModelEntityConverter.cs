using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class MessengerEventModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      MessengerEventModel,
      EventModel,
      MessengerEventEntity,
      EventEntity>(serviceProvider)
{
  public override void InitializeEntity(
    MessengerEventModel model,
    MessengerEventEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.MessengerId = model.MessengerId;
  }

  public override void InitializeModel(
    MessengerEventEntity entity,
    MessengerEventModel model)
  {
    base.InitializeModel(entity, model);
    model.MessengerId = entity.MessengerId;
  }
}
