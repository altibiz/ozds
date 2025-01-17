using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Business.Conversion.Joins;

public class JoinModelEntityConverter
  : ConcreteModelEntityConverter<IJoin, JoinEntity>
{
  protected override JoinEntity ToEntity(IJoin model)
  {
    if (model is NotificationRecipientModel notificationRecipientModel)
    {
      notificationRecipientModel.ToEntity();
    }

    throw new InvalidOperationException(
      $"Unknown join model type {model.GetType()}");
  }

  protected override JoinModel ToModel(JoinEntity entity)
  {
    if (entity is NotificationRecipientEntity notificationRecipientEntity)
    {
      return notificationRecipientEntity.ToModel();
    }

    throw new InvalidOperationException(
      $"Unknown join entity type {entity.GetType()}");
  }
}
