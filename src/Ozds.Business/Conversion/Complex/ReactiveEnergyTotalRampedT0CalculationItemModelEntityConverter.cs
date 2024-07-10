using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

// TODO: split for each concrete type

namespace Ozds.Business.Conversion.Complex;

public class ReactiveEnergyTotalRampedT0CalculationItemModelEntityConverter :
  ModelEntityConverter<
    ReactiveEnergyTotalRampedT0CalculationItemModel,
    ReactiveEnergyTotalRampedT0CalculationItemEntity>
{
  protected override ReactiveEnergyTotalRampedT0CalculationItemEntity ToEntity(
    ReactiveEnergyTotalRampedT0CalculationItemModel model)
  {
    return model switch
    {
      UsageReactiveEnergyTotalRampedT0CalculationItemModel t0Model =>
        t0Model.ToEntity(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }

  protected override ReactiveEnergyTotalRampedT0CalculationItemModel ToModel(
    ReactiveEnergyTotalRampedT0CalculationItemEntity entity)
  {
    return entity switch
    {
      UsageReactiveEnergyTotalRampedT0CalculationItemEntity t0Entity => t0Entity
        .ToModel(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }
}

public static class
  ReactiveEnergyTotalRampedT0CalculationItemModelEntityConverterExtensions
{
  public static UsageReactiveEnergyTotalRampedT0CalculationItemEntity ToEntity(
    this UsageReactiveEnergyTotalRampedT0CalculationItemModel model)
  {
    return new UsageReactiveEnergyTotalRampedT0CalculationItemEntity
    {
      ReactiveImportMin_VARh = model.ReactiveImportMin_VARh,
      ReactiveImportMax_VARh = model.ReactiveImportMax_VARh,
      ReactiveImportAmount_VARh = model.ReactiveImportAmount_VARh,
      ReactiveExportMin_VARh = model.ReactiveExportMin_VARh,
      ReactiveExportMax_VARh = model.ReactiveExportMax_VARh,
      ReactiveExportAmount_VARh = model.ReactiveExportAmount_VARh,
      ActiveImportMin_Wh = model.ActiveImportMin_Wh,
      ActiveImportMax_Wh = model.ActiveImportMax_Wh,
      ActiveImportAmount_Wh = model.ActiveImportAmount_Wh,
      Amount_VARh = model.Amount_VARh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static UsageReactiveEnergyTotalRampedT0CalculationItemModel ToModel(
    this UsageReactiveEnergyTotalRampedT0CalculationItemEntity entity)
  {
    return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
    {
      ReactiveImportMin_VARh = entity.ReactiveImportMin_VARh,
      ReactiveImportMax_VARh = entity.ReactiveImportMax_VARh,
      ReactiveImportAmount_VARh = entity.ReactiveImportAmount_VARh,
      ReactiveExportMin_VARh = entity.ReactiveExportMin_VARh,
      ReactiveExportMax_VARh = entity.ReactiveExportMax_VARh,
      ReactiveExportAmount_VARh = entity.ReactiveExportAmount_VARh,
      ActiveImportMin_Wh = entity.ActiveImportMin_Wh,
      ActiveImportMax_Wh = entity.ActiveImportMax_Wh,
      ActiveImportAmount_Wh = entity.ActiveImportAmount_Wh,
      Amount_VARh = entity.Amount_VARh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }
}
