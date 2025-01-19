using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Implementations.System;

public class NotificationRecipientEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      NotificationRecipientModel,
      JoinModel,
      NotificationRecipientEntity,
      JoinEntity>(serviceProvider)
{
  public override void InitializeEntity(
    NotificationRecipientModel model,
    NotificationRecipientEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.NotificationId = model.NotificationId;
    entity.RepresentativeId = model.RepresentativeId;
    entity.SeenOn = model.SeenOn;
  }

  public override void InitializeModel(
    NotificationRecipientEntity entity,
    NotificationRecipientModel model)
  {
    base.InitializeModel(entity, model);
    model.NotificationId = entity.NotificationId;
    model.RepresentativeId = entity.RepresentativeId;
    model.SeenOn = entity.SeenOn;
  }
}
