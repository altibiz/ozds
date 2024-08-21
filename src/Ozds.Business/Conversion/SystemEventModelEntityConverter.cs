using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class
  SystemEventModelEntityConverter : ModelEntityConverter<SystemEventModel,
  SystemEventEntity>
{
  protected override SystemEventEntity ToEntity(SystemEventModel model)
  {
    return model.ToEntity();
  }

  protected override SystemEventModel ToModel(SystemEventEntity entity)
  {
    return entity.ToModel();
  }
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
      Content = entity.Content
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
      Content = model.Content
    };
  }
}
