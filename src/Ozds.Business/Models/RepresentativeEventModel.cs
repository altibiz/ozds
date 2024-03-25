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
  Id,
  Timestamp,
  Level,
  Description
);

public static class RepresentativeEventModelExtensions
{
  public static RepresentativeEventModel ToModel(
    this RepresentativeEventEntity entity)
  {
    return new RepresentativeEventModel(
      entity.Id,
      entity.Timestamp,
      entity.Level.ToModel(),
      entity.Description,
      entity.RepresentativeId
    );
  }

  public static RepresentativeEventEntity ToEntity(
    this RepresentativeEventModel model)
  {
    return new RepresentativeEventEntity
    {
      Id = model.Id,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description,
      RepresentativeId = model.RepresentativeId
    };
  }
}
