using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record LocationModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string MeasurementLocationId,
  string WhiteMediumCatalogueId,
  string BlueLowCatalogueId,
  string WhiteLowCatalogueId,
  string RedLowCatalogueId,
  string RegulatoryCatalogueId
) : AuditableModel(
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById
)
{
}

public static class LocationModelExtensions
{
  public static LocationModel ToModel(this LocationEntity entity)
  {
    return new LocationModel(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.MeasurementLocationId,
      entity.WhiteMediumCatalogueId,
      entity.BlueLowCatalogueId,
      entity.WhiteLowCatalogueId,
      entity.RedLowCatalogueId,
      entity.RegulatoryCatalogueId
    );
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
