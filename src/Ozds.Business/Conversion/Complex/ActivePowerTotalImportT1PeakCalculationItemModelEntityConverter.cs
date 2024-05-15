using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

// TODO: split for each concrete type

namespace Ozds.Business.Conversion.Complex;

public class ActivePowerTotalImportT1PeakCalculationItemModelEntityConverter :
  ModelEntityConverter<
    ActivePowerTotalImportT1PeakCalculationItemModel,
    ActivePowerTotalImportT1PeakCalculationItemEntity>
{
  protected override ActivePowerTotalImportT1PeakCalculationItemEntity ToEntity(
    ActivePowerTotalImportT1PeakCalculationItemModel model)
  {
    return model switch
    {
      UsageActivePowerTotalImportT1PeakCalculationItemModel t1Model =>
        t1Model.ToEntity(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }

  protected override ActivePowerTotalImportT1PeakCalculationItemModel ToModel(
    ActivePowerTotalImportT1PeakCalculationItemEntity entity)
  {
    return entity switch
    {
      UsageActivePowerTotalImportT1PeakCalculationItemEntity t1Entity =>
        t1Entity
          .ToModel(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }
}

public static class
  ActivePowerTotalImportT1PeakCalculationItemModelEntityConverterExtensions
{
  public static UsageActivePowerTotalImportT1PeakCalculationItemEntity ToEntity(
    this UsageActivePowerTotalImportT1PeakCalculationItemModel model)
  {
    return new UsageActivePowerTotalImportT1PeakCalculationItemEntity
    {
      Peak_kW = model.Peak_kW,
      Amount_kW = model.Amount_kW,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static UsageActivePowerTotalImportT1PeakCalculationItemModel ToModel(
    this UsageActivePowerTotalImportT1PeakCalculationItemEntity entity)
  {
    return new UsageActivePowerTotalImportT1PeakCalculationItemModel
    {
      Peak_kW = entity.Peak_kW,
      Amount_kW = entity.Amount_kW,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }
}
