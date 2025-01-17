using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class INotificationEntityConverter : ConcreteModelEntityConverter<
  INotification, NotificationEntity>
{
  protected override NotificationEntity ToEntity(INotification model)
  {
    return model.ToEntity();
  }

  protected override INotification ToModel(NotificationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class INotificationEntityConverterExtensions
{
  public static NotificationEntity ToEntity(this INotification model)
  {
    if (model is MessengerNotificationModel
      messengerNotificationModel)
    {
      return messengerNotificationModel.ToEntity();
    }

    if (model is SystemNotificationModel
      systemNotificationModel)
    {
      return systemNotificationModel.ToEntity();
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

  public static INotification ToModel(this NotificationEntity entity)
  {
    if (entity is MessengerNotificationEntity
      messengerNotificationEntity)
    {
      return messengerNotificationEntity.ToModel();
    }

    if (entity is SystemNotificationEntity
      systemNotificationEntity)
    {
      return systemNotificationEntity.ToModel();
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
      Topics = entity.Topics.Select(x => x.ToModel()).ToHashSet()
    };
  }
}
