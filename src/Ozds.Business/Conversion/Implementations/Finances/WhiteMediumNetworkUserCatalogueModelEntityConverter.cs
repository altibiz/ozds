using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class WhiteMediumNetworkUserCatalogueModelEntityConverter :
  ConcreteModelEntityConverter<
    WhiteMediumNetworkUserCatalogueModel, WhiteMediumNetworkUserCatalogueEntity>
{
  protected override WhiteMediumNetworkUserCatalogueEntity ToEntity(
    WhiteMediumNetworkUserCatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override WhiteMediumNetworkUserCatalogueModel ToModel(
    WhiteMediumNetworkUserCatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class
  WhiteMediumNetworkUserCatalogueModelEntityConverterExtensions
{
  public static WhiteMediumNetworkUserCatalogueModel ToModel(
    this WhiteMediumNetworkUserCatalogueEntity entity)
  {
    return new WhiteMediumNetworkUserCatalogueModel
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

  public static WhiteMediumNetworkUserCatalogueEntity ToEntity(
    this WhiteMediumNetworkUserCatalogueModel model)
  {
    return new WhiteMediumNetworkUserCatalogueEntity
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
