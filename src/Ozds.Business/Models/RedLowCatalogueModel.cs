using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RedLowCatalogueModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  string LocationId,
  float ActiveEnergyTotalImportT1Price_EUR,
  float ActiveEnergyTotalImportT2Price_EUR,
  float MaxActivePowerTotalImportT1Price_EUR,
  float ReactiveEnergyTotalImportT0Price_EUR,
  float MeterFeePrice_EUR
) : CatalogueModel(
  Id,
  Title,
  CreatedOn,
  CreatedById,
  LastUpdatedOn,
  LastUpdatedById,
  IsDeleted,
  DeletedOn,
  DeletedById,
  LocationId
)
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class RedLowCatalogueModelExtensions
{
  public static RedLowCatalogueModel ToModel(this RedLowCatalogueEntity entity)
  {
    return new RedLowCatalogueModel(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.LocationId,
      entity.ActiveEnergyTotalImportT1Price_EUR,
      entity.ActiveEnergyTotalImportT2Price_EUR,
      entity.MaxActivePowerTotalImportT1Price_EUR,
      entity.ReactiveEnergyTotalImportT0Price_EUR,
      entity.MeterFeePrice_EUR
    );
  }

  public static RedLowCatalogueEntity ToEntity(this RedLowCatalogueModel model)
  {
    return new RedLowCatalogueEntity
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
      ActiveEnergyTotalImportT1Price_EUR =
        model.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT2Price_EUR =
        model.ActiveEnergyTotalImportT2Price_EUR,
      MaxActivePowerTotalImportT1Price_EUR =
        model.MaxActivePowerTotalImportT1Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR =
        model.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}