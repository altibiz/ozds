using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class ReactiveEnergyTotalRampedT0CalculationItemModelEntityConverter :
  ModelEntityConverter<
    ReactiveEnergyTotalRampedT0CalculationItemModel,
    ReactiveEnergyTotalRampedT0CalculationItemEntity>
{
  protected override ReactiveEnergyTotalRampedT0CalculationItemEntity ToEntity(
    ReactiveEnergyTotalRampedT0CalculationItemModel model)
  {
    return model.ToEntity();
  }

  protected override ReactiveEnergyTotalRampedT0CalculationItemModel ToModel(
    ReactiveEnergyTotalRampedT0CalculationItemEntity entity)
  {
    return entity.ToModel();
  }
}

public static class
  ReactiveEnergyTotalRampedT0CalculationItemModelEntityConverterExtensions
{
  public static ReactiveEnergyTotalRampedT0CalculationItemEntity ToEntity(
    this ReactiveEnergyTotalRampedT0CalculationItemModel model)
  {
    return new ReactiveEnergyTotalRampedT0CalculationItemEntity
    {
      ImportMin_VARh = model.ImportMin_VARh,
      ImportMax_VARh = model.ImportMax_VARh,
      ImportAmount_VARh = model.ImportAmount_VARh,
      ExportMin_VARh = model.ExportMin_VARh,
      ExportMax_VARh = model.ExportMax_VARh,
      ExportAmount_VARh = model.ExportAmount_VARh,
      Amount_VARh = model.Amount_VARh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static ReactiveEnergyTotalRampedT0CalculationItemModel ToModel(
    this ReactiveEnergyTotalRampedT0CalculationItemEntity entity)
  {
    return new ReactiveEnergyTotalRampedT0CalculationItemModel
    {
      ImportMin_VARh = entity.ImportMin_VARh,
      ImportMax_VARh = entity.ImportMax_VARh,
      ImportAmount_VARh = entity.ImportAmount_VARh,
      ExportMin_VARh = entity.ExportMin_VARh,
      ExportMax_VARh = entity.ExportMax_VARh,
      ExportAmount_VARh = entity.ExportAmount_VARh,
      Amount_VARh = entity.Amount_VARh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }
}
