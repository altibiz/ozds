using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class LocationMeasurementLocationModel : MeasurementLocationModel
{
}

public static class LocationMeasurementLocationModelExtensions
{
  public static LocationMeasurementLocationModel ToModel(
    this LocationMeasurementLocationEntity entity)
  {
    return new LocationMeasurementLocationModel
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
      MeterId = entity.MeterId
    };
  }

  public static LocationMeasurementLocationEntity ToEntity(
    this LocationMeasurementLocationModel model)
  {
    return new LocationMeasurementLocationEntity
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
      MeterId = model.MeterId
    };
  }
}
