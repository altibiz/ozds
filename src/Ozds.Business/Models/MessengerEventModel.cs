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
  Id: Id,
  Timestamp: Timestamp,
  Level: Level,
  Description: Description
)
{
  public override object ToDbEntity()
  {
    return this.ToEntity();
  }

}

public static class MessengerEventModelExtensions
{
  public static MessengerEventModel ToModel(this MessengerEventEntity entity) =>
    new(
      Id: entity.Id,
      Timestamp: entity.Timestamp,
      Level: entity.Level.ToModel(),
      Description: entity.Description,
      MessengerId: entity.MessengerId
    );

  public static MessengerEventEntity ToEntity(this MessengerEventModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      MessengerId = model.MessengerId
    };
}
