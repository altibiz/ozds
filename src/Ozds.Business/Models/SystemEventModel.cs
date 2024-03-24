using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record SystemEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description
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
};

public static class SystemEventModelExtensions
{
  public static SystemEventModel ToModel(this SystemEventEntity entity) =>
    new(
      Id: entity.Id,
      Timestamp: entity.Timestamp,
      Level: entity.Level.ToModel(),
      Description: entity.Description
    );

  public static SystemEventEntity ToEntity(this SystemEventModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description
    };
}
