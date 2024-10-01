using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Joins;

public class NotificationRecipientModelEntityConverter
  : ModelEntityConverter<NotificationRecipientModel,
    NotificationRecipientEntity>
{
  protected override NotificationRecipientEntity ToEntity(
    NotificationRecipientModel model)
  {
    return model.ToEntity();
  }

  protected override NotificationRecipientModel ToModel(
    NotificationRecipientEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NotificationRepresentativeModelEntityConverterExtensions
{
  public static NotificationRecipientEntity ToEntity(
    this NotificationRecipientModel model)
  {
    return new NotificationRecipientEntity
    {
      NotificationId = model.NotificationId,
      RepresentativeId = model.RepresentativeId,
      SeenOn = model.SeenOn
    };
  }

  public static NotificationRecipientModel ToModel(
    this NotificationRecipientEntity entity)
  {
    return new NotificationRecipientModel
    {
      NotificationId = entity.NotificationId,
      RepresentativeId = entity.RepresentativeId,
      SeenOn = entity.SeenOn
    };
  }
}
