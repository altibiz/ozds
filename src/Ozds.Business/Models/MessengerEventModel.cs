using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record MessengerEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description,
  string MessengerId
) : EventModel(
  Id,
  Timestamp,
  Level,
  Description
);

public static class MessengerEventModelExtensions
{
  public static MessengerEventModel ToModel(this MessengerEventEntity entity)
  {
    return new MessengerEventModel(
      entity.Id,
      entity.Timestamp,
      entity.Level.ToModel(),
      entity.Description,
      entity.MessengerId
    );
  }

  public static MessengerEventEntity ToEntity(this MessengerEventModel model)
  {
    return new MessengerEventEntity
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      MessengerId = model.MessengerId
    };
  }
}
