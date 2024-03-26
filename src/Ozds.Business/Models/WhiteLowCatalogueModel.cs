using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record WhiteLowCatalogueModel(
  string Id,
  string Title,
  DateTimeOffset CreatedOn,
  string? CreatedById,
  DateTimeOffset? LastUpdatedOn,
  string? LastUpdatedById,
  bool IsDeleted,
  DateTimeOffset? DeletedOn,
  string? DeletedById,
  float ActiveEnergyTotalImportT1Price_EUR,
  float ActiveEnergyTotalImportT2Price_EUR,
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
  DeletedById
)
{
  public override IEnumerable<ValidationResult> Validate(
    ValidationContext validationContext)
  {
    throw new NotImplementedException();
  }
}

public static class WhiteLowCatalogueModelExtensions
{
  public static WhiteLowCatalogueModel ToModel(
    this WhiteLowCatalogueEntity entity)
  {
    return new WhiteLowCatalogueModel(
      entity.Id,
      entity.Title,
      entity.CreatedOn,
      entity.CreatedById,
      entity.LastUpdatedOn,
      entity.LastUpdatedById,
      entity.IsDeleted,
      entity.DeletedOn,
      entity.DeletedById,
      entity.ActiveEnergyTotalImportT1Price_EUR,
      entity.ActiveEnergyTotalImportT2Price_EUR,
      entity.ReactiveEnergyTotalImportT0Price_EUR,
      entity.MeterFeePrice_EUR
    );
  }

  public static WhiteLowCatalogueEntity ToEntity(
    this WhiteLowCatalogueModel model)
  {
    return new WhiteLowCatalogueEntity
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
      ActiveEnergyTotalImportT1Price_EUR =
        model.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT2Price_EUR =
        model.ActiveEnergyTotalImportT2Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR =
        model.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
