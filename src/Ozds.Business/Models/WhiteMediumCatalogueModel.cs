using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class WhiteMediumCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT1Price_EUR { get; set; }
  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT2Price_EUR { get; set; }
  [Required]
  [Range(0, double.MaxValue)]
  public required float MaxActivePowerTotalImportT1Price_EUR { get; set; }
  [Required]
  [Range(0, double.MaxValue)]
  public required float ReactiveEnergyTotalImportT0Price_EUR { get; set; }
  [Required]
  [Range(0, double.MaxValue)]
  public required float MeterFeePrice_EUR { get; set; }
}

public static class WhiteMediumCatalogueModelExtensions
{
  public static WhiteMediumCatalogueModel ToModel(
    this WhiteMediumCatalogueEntity entity)
  {
    return new WhiteMediumCatalogueModel()
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
      ActiveEnergyTotalImportT1Price_EUR = entity.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT2Price_EUR = entity.ActiveEnergyTotalImportT2Price_EUR,
      MaxActivePowerTotalImportT1Price_EUR = entity.MaxActivePowerTotalImportT1Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR = entity.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR
    };
  }

  public static WhiteMediumCatalogueEntity ToEntity(
    this WhiteMediumCatalogueModel model)
  {
    return new WhiteMediumCatalogueEntity
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
      MaxActivePowerTotalImportT1Price_EUR =
        model.MaxActivePowerTotalImportT1Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR =
        model.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
