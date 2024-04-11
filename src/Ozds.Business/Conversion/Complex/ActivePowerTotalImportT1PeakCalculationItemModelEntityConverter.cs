using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class ActivePowerTotalImportT1PeakCalculationItemModelEntityConverter :
  ModelEntityConverter<
    ActivePowerTotalImportT1PeakCalculationItemModel,
    ActivePowerTotalImportT1PeakCalculationItemEntity>
{
  protected override ActivePowerTotalImportT1PeakCalculationItemEntity ToEntity(
    ActivePowerTotalImportT1PeakCalculationItemModel model)
  {
    return model.ToEntity();
  }

  protected override ActivePowerTotalImportT1PeakCalculationItemModel ToModel(
    ActivePowerTotalImportT1PeakCalculationItemEntity entity)
  {
    return entity.ToModel();
  }
}

public static class
  ActivePowerTotalImportT1PeakCalculationItemModelEntityConverterExtensions
{
  public static ActivePowerTotalImportT1PeakCalculationItemEntity ToEntity(
    this ActivePowerTotalImportT1PeakCalculationItemModel model)
  {
    return new ActivePowerTotalImportT1PeakCalculationItemEntity
    {
      Peak_W = model.Peak_W,
      Amount_W = model.Amount_W,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static ActivePowerTotalImportT1PeakCalculationItemModel ToModel(
    this ActivePowerTotalImportT1PeakCalculationItemEntity entity)
  {
    return new ActivePowerTotalImportT1PeakCalculationItemModel
    {
      Peak_W = entity.Peak_W,
      Amount_W = entity.Amount_W,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }
}
