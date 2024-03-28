using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class RegulatoryCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float RenewableEnergyFeePrice_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float BusinessUsageFeePrice_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float TaxRate_Percent { get; set; }
}

public static class RegulatoryCatalogueModelExtensions
{
  public static RegulatoryCatalogueModel ToModel(
    this RegulatoryCatalogueEntity entity)
  {
    return new RegulatoryCatalogueModel
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
      ActiveEnergyTotalImportT1Price_EUR =
        entity.ActiveEnergyTotalImportT1Price_EUR,
      ActiveEnergyTotalImportT2Price_EUR =
        entity.ActiveEnergyTotalImportT2Price_EUR,
      RenewableEnergyFeePrice_EUR = entity.RenewableEnergyFeePrice_EUR,
      BusinessUsageFeePrice_EUR = entity.BusinessUsageFeePrice_EUR,
      TaxRate_Percent = entity.TaxRate_Percent
    };
  }

  public static RegulatoryCatalogueEntity ToEntity(
    this RegulatoryCatalogueModel model)
  {
    return new RegulatoryCatalogueEntity
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
      RenewableEnergyFeePrice_EUR = model.RenewableEnergyFeePrice_EUR,
      BusinessUsageFeePrice_EUR = model.BusinessUsageFeePrice_EUR,
      TaxRate_Percent = model.TaxRate_Percent
    };
  }
}
