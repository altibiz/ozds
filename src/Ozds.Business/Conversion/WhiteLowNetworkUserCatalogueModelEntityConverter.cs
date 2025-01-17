using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class WhiteLowNetworkUserCatalogueModelEntityConverter :
  ConcreteModelEntityConverter<
    WhiteLowNetworkUserCatalogueModel, WhiteLowNetworkUserCatalogueEntity>
{
  protected override WhiteLowNetworkUserCatalogueEntity ToEntity(
    WhiteLowNetworkUserCatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override WhiteLowNetworkUserCatalogueModel ToModel(
    WhiteLowNetworkUserCatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class WhiteLowNetworkUserCatalogueModelEntityConverterExtensions
{
  public static WhiteLowNetworkUserCatalogueModel ToModel(
    this WhiteLowNetworkUserCatalogueEntity entity)
  {
    return new WhiteLowNetworkUserCatalogueModel
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
      ReactiveEnergyTotalRampedT0Price_EUR =
        entity.ReactiveEnergyTotalRampedT0Price_EUR,
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR
    };
  }

  public static WhiteLowNetworkUserCatalogueEntity ToEntity(
    this WhiteLowNetworkUserCatalogueModel model)
  {
    return new WhiteLowNetworkUserCatalogueEntity
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
      ReactiveEnergyTotalRampedT0Price_EUR =
        model.ReactiveEnergyTotalRampedT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
