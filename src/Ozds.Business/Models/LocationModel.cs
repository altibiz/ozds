using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class LocationModel : AuditableModel
{
  [Required] public required string MeasurementLocationId { get; set; }

  [Required] public required string WhiteMediumCatalogueId { get; set; }

  [Required] public required string BlueLowCatalogueId { get; set; }

  [Required] public required string WhiteLowCatalogueId { get; set; }

  [Required] public required string RedLowCatalogueId { get; set; }

  [Required] public required string RegulatoryCatalogueId { get; set; }
}

public static class LocationModelExtensions
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
      MeasurementLocationId = entity.MeasurementLocationId,
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
      MeasurementLocationId = model.MeasurementLocationId,
      WhiteMediumCatalogueId = model.WhiteMediumCatalogueId,
      BlueLowCatalogueId = model.BlueLowCatalogueId,
      WhiteLowCatalogueId = model.WhiteLowCatalogueId,
      RedLowCatalogueId = model.RedLowCatalogueId,
      RegulatoryCatalogueId = model.RegulatoryCatalogueId
    };
  }
}
