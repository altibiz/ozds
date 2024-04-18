using Ozds.Business.Conversion.Base;
using Ozds.Business.Conversion.Complex;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class
  LocationModelEntityConverter : ModelEntityConverter<LocationModel,
  LocationEntity>
{
  protected override LocationEntity ToEntity(LocationModel model)
  {
    return model.ToEntity();
  }

  protected override LocationModel ToModel(LocationEntity entity)
  {
    return entity.ToModel();
  }
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
      WhiteMediumNetworkUserCatalogueId =
        entity.WhiteMediumNetworkUserCatalogueId,
      BlueLowNetworkUserCatalogueId = entity.BlueLowNetworkUserCatalogueId,
      WhiteLowNetworkUserCatalogueId = entity.WhiteLowNetworkUserCatalogueId,
      RedLowNetworkUserCatalogueId = entity.RedLowNetworkUserCatalogueId,
      RegulatoryCatalogueId = entity.RegulatoryCatalogueId,
      LegalPerson = entity.LegalPerson.ToModel()
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
      WhiteMediumNetworkUserCatalogueId =
        model.WhiteMediumNetworkUserCatalogueId,
      BlueLowNetworkUserCatalogueId = model.BlueLowNetworkUserCatalogueId,
      WhiteLowNetworkUserCatalogueId = model.WhiteLowNetworkUserCatalogueId,
      RedLowNetworkUserCatalogueId = model.RedLowNetworkUserCatalogueId,
      RegulatoryCatalogueId = model.RegulatoryCatalogueId,
      LegalPerson = model.LegalPerson.ToEntity()
    };
  }
}
