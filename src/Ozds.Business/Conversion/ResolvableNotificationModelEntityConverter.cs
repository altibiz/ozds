using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class ResolvableNotificationModelEntityConverter :
  ModelEntityConverter<
    ResolvableNotificationModel,
    ResolvableNotificationEntity>
{
  protected override ResolvableNotificationEntity ToEntity(
    ResolvableNotificationModel model)
  {
    return new ResolvableNotificationEntity
    {
      Id = model.Id,
      Title = model.Title,
      Summary = model.Summary,
      Content = model.Content,
      Timestamp = model.Timestamp,
      EventId = model.EventId,
      ResolvedById = model.ResolvedById,
      ResolvedOn = model.ResolvedOn,
      Topic = model.Topic.ToEntity()
    };
  }

  protected override ResolvableNotificationModel ToModel(
    ResolvableNotificationEntity entity)
  {
    return new ResolvableNotificationModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Summary = entity.Summary,
      Content = entity.Content,
      Timestamp = entity.Timestamp,
      EventId = entity.EventId,
      ResolvedById = entity.ResolvedById,
      ResolvedOn = entity.ResolvedOn,
      Topic = entity.Topic.ToModel()
    };
  }
}
