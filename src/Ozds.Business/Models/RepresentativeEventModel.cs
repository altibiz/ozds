using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RepresentativeEventModel(
  string Id,
  DateTimeOffset Timestamp,
  LevelModel Level,
  string Description,
  string RepresentativeId
) : EventModel(
  Id: Id,
  Timestamp: Timestamp,
  Level: Level,
  Description: Description
);

public static class RepresentativeEventModelExtensions
{
  public static RepresentativeEventModel ToModel(this RepresentativeEventEntity entity) =>
    new(
      Id: entity.Id,
      Timestamp: entity.Timestamp,
      Level: entity.Level.ToModel(),
      Description: entity.Description,
      RepresentativeId: entity.RepresentativeId
    );

  public static RepresentativeEventEntity ToEntity(this RepresentativeEventModel model) =>
    new()
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      RepresentativeId = model.RepresentativeId
    };
}
