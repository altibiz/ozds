using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.System;

public class TopicModelEntityConverter
  : ConcreteModelEntityConverter<TopicModel, TopicEntity>
{
  public override TopicEntity ToEntity(TopicModel model)
  {
    return model switch
    {
      TopicModel.All => TopicEntity.All,
      TopicModel.Messenger => TopicEntity.Messenger,
      TopicModel.MessengerInactivity => TopicEntity.MessengerInactivity,
      TopicModel.InvalidPush => TopicEntity.InvalidPush,
      TopicModel.Error => TopicEntity.Error,
      TopicModel.NetworkUserInvoiceState => TopicEntity.NetworkUserInvoiceState,
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }

  public override TopicModel ToModel(TopicEntity entity)
  {
    return entity switch
    {
      TopicEntity.All => TopicModel.All,
      TopicEntity.Messenger => TopicModel.Messenger,
      TopicEntity.MessengerInactivity => TopicModel.MessengerInactivity,
      TopicEntity.InvalidPush => TopicModel.InvalidPush,
      TopicEntity.Error => TopicModel.Error,
      TopicEntity.NetworkUserInvoiceState => TopicModel.NetworkUserInvoiceState,
      _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
    };
  }
}
