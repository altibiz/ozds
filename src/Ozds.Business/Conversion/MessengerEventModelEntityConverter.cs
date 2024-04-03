using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class MessengerEventModelEntityConverter : ModelEntityConverter<
  MessengerEventModel, MessengerEventEntity>
{
  protected override MessengerEventEntity ToEntity(MessengerEventModel model)
  {
    return model.ToEntity();
  }

  protected override MessengerEventModel ToModel(MessengerEventEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MessengerEventModelExtensions
{
  public static MessengerEventModel ToModel(this MessengerEventEntity entity)
  {
    return new MessengerEventModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Description = entity.Description,
      MessengerId = entity.MessengerId
    };
  }

  public static MessengerEventEntity ToEntity(this MessengerEventModel model)
  {
    return new MessengerEventEntity
    {
      Id = model.Id,
      Title = model.Title,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      MessengerId = model.MessengerId
    };
  }
}
