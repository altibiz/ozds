using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class LocationModelEntityConverter : ModelEntityConverter<LocationModel, LocationEntity>
{
  protected override LocationEntity ToEntity(LocationModel model) =>
    model.ToEntity();

  protected override LocationModel ToModel(LocationEntity entity) =>
    entity.ToModel();
}

public static class LocationModelEntityConverterExtensions
{
  public static LocationModel ToModel(this LocationEntity entity)
  {
    return new LocationModel
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
      WhiteMediumCatalogueId = entity.WhiteMediumCatalogueId,
      BlueLowCatalogueId = entity.BlueLowCatalogueId,
      WhiteLowCatalogueId = entity.WhiteLowCatalogueId,
      RedLowCatalogueId = entity.RedLowCatalogueId,
      RegulatoryCatalogueId = entity.RegulatoryCatalogueId
    };
  }

  public static LocationEntity ToEntity(this LocationModel model)
  {
    return new LocationEntity
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
      WhiteMediumCatalogueId = model.WhiteMediumCatalogueId,
      BlueLowCatalogueId = model.BlueLowCatalogueId,
      WhiteLowCatalogueId = model.WhiteLowCatalogueId,
      RedLowCatalogueId = model.RedLowCatalogueId,
      RegulatoryCatalogueId = model.RegulatoryCatalogueId
    };
  }
}
