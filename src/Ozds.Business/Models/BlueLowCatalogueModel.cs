using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class BlueLowCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT0Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float ReactiveEnergyTotalImportT0Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float MeterFeePrice_EUR { get; set; }
}

public static class BlueLowCatalogueModelExtensions
{
  public static BlueLowCatalogueModel ToModel(
    this BlueLowCatalogueEntity entity)
  {
    return new BlueLowCatalogueModel
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
      ActiveEnergyTotalImportT0Price_EUR =
        entity.ActiveEnergyTotalImportT0Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR =
        entity.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR
    };
  }

  public static BlueLowCatalogueEntity ToEntity(
    this BlueLowCatalogueModel model)
  {
    return new BlueLowCatalogueEntity
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
      ActiveEnergyTotalImportT0Price_EUR =
        model.ActiveEnergyTotalImportT0Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR =
        model.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
