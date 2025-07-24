using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class MeterNotificationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  MeterNotificationModel,
  ResolvableNotificationModel,
  MeterNotificationEntity,
  ResolvableNotificationEntity>(serviceProvider)
{
  public override void InitializeEntity(
    MeterNotificationModel model,
    MeterNotificationEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.MeterId = model.MeterId;
  }

  public override void InitializeModel(
    MeterNotificationEntity entity,
    MeterNotificationModel model)
  {
    base.InitializeModel(entity, model);
    model.MeterId = entity.MeterId;
  }
}
