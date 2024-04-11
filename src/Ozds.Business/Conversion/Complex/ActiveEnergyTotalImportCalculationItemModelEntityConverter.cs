using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class ActiveEnergyTotalImportCalculationItemModelEntityConverter :
  ModelEntityConverter<
    ActiveEnergyTotalImportCalculationItemModel,
    ActiveEnergyTotalImportCalculationItemEntity>
{
  protected override ActiveEnergyTotalImportCalculationItemEntity ToEntity(
    ActiveEnergyTotalImportCalculationItemModel model)
  {
    return model switch
    {
      ActiveEnergyTotalImportT0CalculationItemModel t0Model =>
        t0Model.ToEntity(),
      ActiveEnergyTotalImportT1CalculationItemModel t1Model =>
        t1Model.ToEntity(),
      ActiveEnergyTotalImportT2CalculationItemModel t2Model =>
        t2Model.ToEntity(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }

  protected override ActiveEnergyTotalImportCalculationItemModel ToModel(
    ActiveEnergyTotalImportCalculationItemEntity entity)
  {
    return entity switch
    {
      ActiveEnergyTotalImportT0CalculationItemEntity t0Entity => t0Entity
        .ToModel(),
      ActiveEnergyTotalImportT1CalculationItemEntity t1Entity => t1Entity
        .ToModel(),
      ActiveEnergyTotalImportT2CalculationItemEntity t2Entity => t2Entity
        .ToModel(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }
}

public static class
  ActiveEnergyTotalImportCalculationItemModelEntityConverterExtensions
{
  public static ActiveEnergyTotalImportT0CalculationItemModel ToModel(
    this ActiveEnergyTotalImportT0CalculationItemEntity entity)
  {
    return new ActiveEnergyTotalImportT0CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static ActiveEnergyTotalImportT1CalculationItemModel ToModel(
    this ActiveEnergyTotalImportT1CalculationItemEntity entity)
  {
    return new ActiveEnergyTotalImportT1CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static ActiveEnergyTotalImportT2CalculationItemModel ToModel(
    this ActiveEnergyTotalImportT2CalculationItemEntity entity)
  {
    return new ActiveEnergyTotalImportT2CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static ActiveEnergyTotalImportT0CalculationItemEntity ToEntity(
    this ActiveEnergyTotalImportT0CalculationItemModel model)
  {
    return new ActiveEnergyTotalImportT0CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static ActiveEnergyTotalImportT1CalculationItemEntity ToEntity(
    this ActiveEnergyTotalImportT1CalculationItemModel model)
  {
    return new ActiveEnergyTotalImportT1CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static ActiveEnergyTotalImportT2CalculationItemEntity ToEntity(
    this ActiveEnergyTotalImportT2CalculationItemModel model)
  {
    return new ActiveEnergyTotalImportT2CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }
}
