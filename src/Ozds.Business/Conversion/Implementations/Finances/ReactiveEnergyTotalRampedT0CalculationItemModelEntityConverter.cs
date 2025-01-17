using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class ReactiveEnergyTotalRampedT0CalculationItemModelEntityConverter :
  ConcreteModelEntityConverter<
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
      ReactiveImportMin_kVARh = model.ReactiveImportMin_kVARh,
      ReactiveImportMax_kVARh = model.ReactiveImportMax_kVARh,
      ReactiveImportAmount_kVARh = model.ReactiveImportAmount_kVARh,
      ReactiveExportMin_kVARh = model.ReactiveExportMin_kVARh,
      ReactiveExportMax_kVARh = model.ReactiveExportMax_kVARh,
      ReactiveExportAmount_kVARh = model.ReactiveExportAmount_kVARh,
      ActiveImportMin_kWh = model.ActiveImportMin_kWh,
      ActiveImportMax_kWh = model.ActiveImportMax_kWh,
      ActiveImportAmount_kWh = model.ActiveImportAmount_kWh,
      Amount_kVARh = model.Amount_kVARh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static UsageReactiveEnergyTotalRampedT0CalculationItemModel ToModel(
    this UsageReactiveEnergyTotalRampedT0CalculationItemEntity entity)
  {
    return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
    {
      ReactiveImportMin_kVARh = entity.ReactiveImportMin_kVARh,
      ReactiveImportMax_kVARh = entity.ReactiveImportMax_kVARh,
      ReactiveImportAmount_kVARh = entity.ReactiveImportAmount_kVARh,
      ReactiveExportMin_kVARh = entity.ReactiveExportMin_kVARh,
      ReactiveExportMax_kVARh = entity.ReactiveExportMax_kVARh,
      ReactiveExportAmount_kVARh = entity.ReactiveExportAmount_kVARh,
      ActiveImportMin_kWh = entity.ActiveImportMin_kWh,
      ActiveImportMax_kWh = entity.ActiveImportMax_kWh,
      ActiveImportAmount_kWh = entity.ActiveImportAmount_kWh,
      Amount_kVARh = entity.Amount_kVARh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }
}
