using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public record RegulatoryCatalogueModel(
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
  float RenewableEnergyFeePrice_EUR,
  float BusinessUsageFeePrice_EUR,
  float TaxRate_Percent
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
}

public static class RegulatoryCatalogueModelExtensions
{
  public static RegulatoryCatalogueModel ToModel(
    this RegulatoryCatalogueEntity entity)
  {
    return new RegulatoryCatalogueModel(
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
      entity.RenewableEnergyFeePrice_EUR,
      entity.BusinessUsageFeePrice_EUR,
      entity.TaxRate_Percent
    );
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
