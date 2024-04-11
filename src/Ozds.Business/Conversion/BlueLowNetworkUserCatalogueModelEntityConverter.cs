using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class BlueLowNetworkUserCatalogueModelEntityConverter :
  ModelEntityConverter<
    BlueLowNetworkUserCatalogueModel, BlueLowNetworkUserCatalogueEntity>
{
  protected override BlueLowNetworkUserCatalogueEntity ToEntity(
    BlueLowNetworkUserCatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override BlueLowNetworkUserCatalogueModel ToModel(
    BlueLowNetworkUserCatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class BlueLowNetworkUserCatalogueModelEntityConverterExtensions
{
  public static BlueLowNetworkUserCatalogueModel ToModel(
    this BlueLowNetworkUserCatalogueEntity entity)
  {
    return new BlueLowNetworkUserCatalogueModel
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
      ReactiveEnergyTotalRampedT0Price_EUR =
        entity.ReactiveEnergyTotalRampedT0Price_EUR,
      MeterFeePrice_EUR = entity.MeterFeePrice_EUR
    };
  }

  public static BlueLowNetworkUserCatalogueEntity ToEntity(
    this BlueLowNetworkUserCatalogueModel model)
  {
    return new BlueLowNetworkUserCatalogueEntity
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
      ReactiveEnergyTotalRampedT0Price_EUR =
        model.ReactiveEnergyTotalRampedT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
