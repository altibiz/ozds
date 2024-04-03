using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class NetworkUserModelEntityConverter : ModelEntityConverter<NetworkUserModel, NetworkUserEntity>
{
  protected override NetworkUserEntity ToEntity(NetworkUserModel model) =>
    model.ToEntity();

  protected override NetworkUserModel ToModel(NetworkUserEntity entity) =>
    entity.ToModel();
}

public static class NetworkUserModelEntityConverterExtensions
{
  public static NetworkUserModel ToModel(this NetworkUserEntity entity)
  {
    return new NetworkUserModel
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

  public static NetworkUserEntity ToEntity(this NetworkUserModel model)
  {
    return new NetworkUserEntity
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
