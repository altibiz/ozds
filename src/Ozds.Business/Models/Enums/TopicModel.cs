using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Models.Enums;

public enum TopicModel
{
  All,
  Messenger,
  MessengerInactivity,
  InvalidPush,
  Error,
  NetworkUserInvoiceState
}

public static class TopicModelExtensions
{
  public static TopicModel ToModel(this TopicEntity entity)
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

  public static TopicEntity ToEntity(this TopicModel model)
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

  public static string ToTitle(this TopicModel model)
  {
    return model switch
    {
      TopicModel.All => "General",
      TopicModel.Messenger => "Messenger",
      TopicModel.MessengerInactivity => "Messenger inactivity",
      TopicModel.InvalidPush => "Invalid push",
      TopicModel.Error => "Error",
      _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
    };
  }
}
