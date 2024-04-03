using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RegulatoryCatalogueModelEntityConverter : ModelEntityConverter<
  RegulatoryCatalogueModel, RegulatoryCatalogueEntity>
{
  protected override RegulatoryCatalogueEntity ToEntity(
    RegulatoryCatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override RegulatoryCatalogueModel ToModel(
    RegulatoryCatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class RegulatoryCatalogueModelEntityConverterExtensions
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
