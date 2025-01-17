using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class
  MessengerModelEntityConverter : ConcreteModelEntityConverter<IMessenger,
  MessengerEntity>
{
  protected override MessengerEntity ToEntity(IMessenger model)
  {
    return model.ToEntity();
  }

  protected override IMessenger ToModel(MessengerEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MessengerModelEntityConverterExtensions
{
  public static IMessenger ToModel(this MessengerEntity entity)
  {
    if (entity is PidgeonMessengerEntity pidgeonMessenger)
    {
      return pidgeonMessenger.ToModel();
    }

    throw new NotImplementedException();
  }

  public static MessengerEntity ToEntity(this IMessenger model)
  {
    if (model is PidgeonMessengerModel pidgeonMessenger)
    {
      return pidgeonMessenger.ToEntity();
    }

    throw new NotImplementedException();
  }
}
