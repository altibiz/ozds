using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class MessengerNotificationModelEntityConverter :
  ModelEntityConverter<MessengerNotificationModel, MessengerNotificationEntity>
{
  protected override MessengerNotificationEntity ToEntity(
    MessengerNotificationModel model)
  {
    return model.ToEntity();
  }

  protected override MessengerNotificationModel ToModel(
    MessengerNotificationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MessengerInactivityNotificationModelEntityConverterExtensions
{
  public static MessengerNotificationModel ToModel(
    this MessengerNotificationEntity entity)
  {
    return new()
    {
      Id = entity.Id,
      Title = entity.Title,
      Summary = entity.Summary,
      Content = entity.Content,
      Timestamp = entity.Timestamp,
      EventId = entity.EventId,
      ResolvedById = entity.ResolvedById,
      ResolvedOn = entity.ResolvedOn,
      Topics = entity.Topics.Select(x => x.ToModel()).ToList(),
      MessengerId = entity.MessengerId
    };
  }

  public static MessengerNotificationEntity ToEntity(
    this MessengerNotificationModel model)
  {
    return new()
    {
      Id = model.Id,
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      Timestamp = model.Timestamp,
      EventId = model.EventId,
      ResolvedById = model.ResolvedById,
      ResolvedOn = model.ResolvedOn,
      Topics = model.Topics.Select(x => x.ToEntity()).ToList(),
      MessengerId = model.MessengerId
    };
  }
}
