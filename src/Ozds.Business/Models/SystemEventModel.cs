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
  Id,
  Timestamp,
  Level,
  Description
);

public static class SystemEventModelExtensions
{
  public static SystemEventModel ToModel(this SystemEventEntity entity)
  {
    return new SystemEventModel(
      entity.Id,
      entity.Timestamp,
      entity.Level.ToModel(),
      entity.Description
    );
  }

  public static SystemEventEntity ToEntity(this SystemEventModel model)
  {
    return new SystemEventEntity
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description
    };
  }
}
