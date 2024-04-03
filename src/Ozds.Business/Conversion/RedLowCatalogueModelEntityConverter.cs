using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RedLowCatalogueModelEntityConverter : ModelEntityConverter<RedLowCatalogueModel, RedLowCatalogueEntity>
{
  protected override RedLowCatalogueEntity ToEntity(RedLowCatalogueModel model) =>
    model.ToEntity();

  protected override RedLowCatalogueModel ToModel(RedLowCatalogueEntity entity) =>
    entity.ToModel();
}

public static class RedLowCatalogueModelEntityConverterExtensions
{
  public static RedLowCatalogueModel ToModel(this RedLowCatalogueEntity entity)
  {
    return new RedLowCatalogueModel
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
      MaxActivePowerTotalImportT1Price_EUR =
        entity.MaxActivePowerTotalImportT1Price_EUR,
      ReactiveEnergyTotalImportT0Price_EUR =
        entity.ReactiveEnergyTotalImportT0Price_EUR,
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR
    };
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
