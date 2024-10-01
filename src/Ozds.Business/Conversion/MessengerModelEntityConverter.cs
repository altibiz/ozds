using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class
  MessengerModelEntityConverter : ModelEntityConverter<MessengerModel,
  MessengerEntity>
{
  protected override MessengerEntity ToEntity(MessengerModel model)
  {
    return model.ToEntity();
  }

  protected override MessengerModel ToModel(MessengerEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MessengerModelEntityConverterExtensions
{
  public static MessengerModel ToModel(this MessengerEntity entity)
  {
    if (entity is PidgeonMessengerEntity pidgeonMessenger)
    {
      return pidgeonMessenger.ToModel();
    }

    throw new NotImplementedException();
  }

  public static MessengerEntity ToEntity(this MessengerModel model)
  {
    if (model is PidgeonMessengerModel pidgeonMessenger)
    {
      return pidgeonMessenger.ToEntity();
    }

    throw new NotImplementedException();
  }
}
