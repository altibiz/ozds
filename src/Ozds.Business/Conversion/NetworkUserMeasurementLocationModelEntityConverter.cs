using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class NetworkUserMeasurementLocationModelEntityConverter :
  ModelEntityConverter<NetworkUserMeasurementLocationModel,
    NetworkUserMeasurementLocationEntity>
{
  protected override NetworkUserMeasurementLocationEntity ToEntity(
    NetworkUserMeasurementLocationModel model)
  {
    return model.ToEntity();
  }

  protected override NetworkUserMeasurementLocationModel ToModel(
    NetworkUserMeasurementLocationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NetworkUserMeasurementLocationModelEntityConverterExtensions
{
  public static NetworkUserMeasurementLocationModel ToModel(
    this NetworkUserMeasurementLocationEntity entity)
  {
    return new NetworkUserMeasurementLocationModel
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
      MeterId = entity.MeterId,
      NetworkUserId = entity.NetworkUserId,
      NetworkUserCatalogueId = entity.NetworkUserCatalogueId
    };
  }

  public static NetworkUserMeasurementLocationEntity ToEntity(
    this NetworkUserMeasurementLocationModel model)
  {
    return new NetworkUserMeasurementLocationEntity
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
      MeterId = model.MeterId,
      NetworkUserId = model.NetworkUserId,
      NetworkUserCatalogueId = model.NetworkUserCatalogueId
    };
  }
}
