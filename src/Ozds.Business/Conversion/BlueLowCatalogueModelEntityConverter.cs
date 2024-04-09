using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class BlueLowCatalogueModelEntityConverter : ModelEntityConverter<
  BlueLowCatalogueModel, BlueLowCatalogueEntity>
{
  protected override BlueLowCatalogueEntity ToEntity(
    BlueLowCatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override BlueLowCatalogueModel ToModel(
    BlueLowCatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class BlueLowCatalogueModelEntityConverterExtensions
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
      ReactiveEnergyTotalRampedT0Price_EUR =
        entity.ReactiveEnergyTotalRampedT0Price_EUR,
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
      ReactiveEnergyTotalRampedT0Price_EUR =
        model.ReactiveEnergyTotalRampedT0Price_EUR,
      MeterFeePrice_EUR = model.MeterFeePrice_EUR
    };
  }
}
