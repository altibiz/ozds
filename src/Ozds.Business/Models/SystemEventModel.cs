using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class SystemEventModel : EventModel
{
}

public static class SystemEventModelExtensions
{
  public static SystemEventModel ToModel(this SystemEventEntity entity)
  {
    return new SystemEventModel
    {
      Id = entity.Id,
      Title = entity.Title,
      Timestamp = entity.Timestamp,
      Level = entity.Level.ToModel(),
      Description = entity.Description
    };
  }

  public static SystemEventEntity ToEntity(this SystemEventModel model)
  {
    return new SystemEventEntity
    {
      Id = model.Id,
      Title = model.Title,
      Timestamp = model.Timestamp,
      Level = model.Level.ToEntity(),
      Description = model.Description
    };
  }
}
