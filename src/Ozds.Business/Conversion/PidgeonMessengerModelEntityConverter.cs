using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class
  PidgeonMessengerModelEntityConverter : ModelEntityConverter<PidgeonMessengerModel,
  PidgeonMessengerEntity>
{
  protected override PidgeonMessengerEntity ToEntity(PidgeonMessengerModel model)
  {
    return model.ToEntity();
  }

  protected override PidgeonMessengerModel ToModel(PidgeonMessengerEntity entity)
  {
    return entity.ToModel();
  }
}

public static class PidgeonMessengerModelEntityConverterExtensions
{
  public static PidgeonMessengerModel ToModel(this PidgeonMessengerEntity entity)
  {
    return new PidgeonMessengerModel
    {
      Id = entity.Id,
      Title = entity.Title,
      CreatedOn = entity.CreatedOn,
      CreatedById = entity.CreatedById,
      LastUpdatedOn = entity.LastUpdatedOn,
      LastUpdatedById = entity.LastUpdatedById,
      IsDeleted = entity.IsDeleted,
      DeletedOn = entity.DeletedOn,
      DeletedById = entity.DeletedById,
      LocationId = entity.LocationId,
      MaxInactivityPeriod = entity.MaxInactivityPeriod.ToModel(),
      PushDelayPeriod = entity.PushDelayPeriod.ToModel()
    };
  }

  public static PidgeonMessengerEntity ToEntity(this PidgeonMessengerModel model)
  {
    return new PidgeonMessengerEntity
    {
      Id = model.Id,
      Title = model.Title,
      CreatedOn = model.CreatedOn,
      CreatedById = model.CreatedById,
      LastUpdatedOn = model.LastUpdatedOn,
      LastUpdatedById = model.LastUpdatedById,
      IsDeleted = model.IsDeleted,
      DeletedOn = model.DeletedOn,
      DeletedById = model.DeletedById,
      LocationId = model.LocationId,
      MaxInactivityPeriod = model.MaxInactivityPeriod.ToEntity(),
      PushDelayPeriod = model.PushDelayPeriod.ToEntity()
    };
  }
}
