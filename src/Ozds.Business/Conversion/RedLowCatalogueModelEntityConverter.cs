using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class RedLowCatalogueModelEntityConverter : ModelEntityConverter<
  RedLowCatalogueModel, RedLowCatalogueEntity>
{
  protected override RedLowCatalogueEntity ToEntity(RedLowCatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override RedLowCatalogueModel ToModel(RedLowCatalogueEntity entity)
  {
    return entity.ToModel();
  }
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
      ActivePowerTotalImportT1Price_EUR =
        entity.ActivePowerTotalImportT1Price_EUR,
      ReactiveEnergyTotalRampedT0Price_EUR =
        entity.ReactiveEnergyTotalRampedT0Price_EUR,
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
      ActivePowerTotalImportT1Price_EUR =
        model.ActivePowerTotalImportT1Price_EUR,
      ReactiveEnergyTotalRampedT0Price_EUR =
        model.ReactiveEnergyTotalRampedT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
