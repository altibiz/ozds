using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class
  NetworkUserModelEntityConverter : ConcreteModelEntityConverter<NetworkUserModel,
  NetworkUserEntity>
{
  protected override NetworkUserEntity ToEntity(NetworkUserModel model)
  {
    return model.ToEntity();
  }

  protected override NetworkUserModel ToModel(NetworkUserEntity entity)
  {
    return entity.ToModel();
  }
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
      LocationId = entity.LocationId,
      LegalPerson = entity.LegalPerson.ToModel(),
      AltiBizSubProjectCode = entity.AltiBizSubProjectCode
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
      LocationId = model.LocationId,
      LegalPerson = model.LegalPerson.ToEntity(),
      AltiBizSubProjectCode = model.AltiBizSubProjectCode
    };
  }
}
