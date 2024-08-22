using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Joins;

public class NotificationRepresentativeModelEntityConverter
  : ModelEntityConverter<NotificationRepresentativeModel, NotificationRepresentativeEntity>
{
  protected override NotificationRepresentativeEntity ToEntity(
    NotificationRepresentativeModel model)
  {
    return new NotificationRepresentativeEntity
    {
      NotificationId = model.NotificationId,
      RepresentativeId = model.RepresentativeId,
      SeenOn = model.SeenOn
    };
  }

  protected override NotificationRepresentativeModel ToModel(
    NotificationRepresentativeEntity entity)
  {
    return new NotificationRepresentativeModel
    {
      NotificationId = entity.NotificationId,
      RepresentativeId = entity.RepresentativeId,
      SeenOn = entity.SeenOn
    };
  }
}
