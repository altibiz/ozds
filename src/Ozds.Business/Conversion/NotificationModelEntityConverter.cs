using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class NotificationModelEntityConverter : ModelEntityConverter<
  NotificationModel, NotificationEntity>
{
  protected override NotificationEntity ToEntity(NotificationModel model)
  {
    return model.ToEntity();
  }

  protected override NotificationModel ToModel(NotificationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NotificationModelEntityConverterExtensions
{
  public static NotificationEntity ToEntity(this NotificationModel model)
  {
    if (model is MessengerNotificationModel messengerInactivityNotificationModel)
    {
      return messengerInactivityNotificationModel.ToEntity();
    }

    if (model is ResolvableNotificationModel resolvableNotificationModel)
    {
      return resolvableNotificationModel.ToEntity();
    }

    return new NotificationEntity
    {
      Id = model.Id,
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      Timestamp = model.Timestamp,
      EventId = model.EventId,
      Topics = model.Topics.Select(x => x.ToEntity()).ToList()
    };
  }

  public static NotificationModel ToModel(this NotificationEntity entity)
  {
    if (entity is MessengerNotificationEntity messengerInactivityNotificationEntity)
    {
      return messengerInactivityNotificationEntity.ToModel();
    }

    if (entity is ResolvableNotificationEntity resolvableNotificationEntity)
    {
      return resolvableNotificationEntity.ToModel();
    }

    return new NotificationModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Summary = entity.Summary,
      Content = entity.Content,
      Timestamp = entity.Timestamp,
      EventId = entity.EventId,
      Topics = entity.Topics.Select(x => x.ToModel()).ToList()
    };
  }
}
