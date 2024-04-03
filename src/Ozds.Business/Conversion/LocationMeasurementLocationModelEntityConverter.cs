using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class LocationMeasurementLocationModelEntityConverter : ModelEntityConverter<LocationMeasurementLocationModel, LocationMeasurementLocationEntity>
{
  protected override LocationMeasurementLocationEntity ToEntity(LocationMeasurementLocationModel model) =>
    model.ToEntity();

  protected override LocationMeasurementLocationModel ToModel(LocationMeasurementLocationEntity entity) =>
    entity.ToModel();
}

public static class LocationMeasurementLocationModelEntityConverterExtensions
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
