using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class
  MessengerModelEntityConverter : ModelEntityConverter<MessengerModel,
  MessengerEntity>
{
  protected override MessengerEntity ToEntity(MessengerModel model)
  {
    return model.ToEntity();
  }

  protected override MessengerModel ToModel(MessengerEntity entity)
  {
    return entity.ToModel();
  }
}

public static class MessengerModelEntityConverterExtensions
{
  public static MessengerModel ToModel(this MessengerEntity entity)
  {
    return new MessengerModel
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
      LocationId = entity.LocationId
    };
  }

  public static MessengerEntity ToEntity(this MessengerModel model)
  {
    return new MessengerEntity
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
      LocationId = model.LocationId
    };
  }
}
