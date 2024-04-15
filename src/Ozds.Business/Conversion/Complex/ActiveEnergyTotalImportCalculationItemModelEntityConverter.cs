using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

// TODO: split for each concrete type

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
      UsageActiveEnergyTotalImportT0CalculationItemModel t0Model =>
        t0Model.ToEntity(),
      UsageActiveEnergyTotalImportT1CalculationItemModel t1Model =>
        t1Model.ToEntity(),
      UsageActiveEnergyTotalImportT2CalculationItemModel t2Model =>
        t2Model.ToEntity(),
      SupplyActiveEnergyTotalImportT1CalculationItemModel t1Model =>
        t1Model.ToEntity(),
      SupplyActiveEnergyTotalImportT2CalculationItemModel t2Model =>
        t2Model.ToEntity(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }

  protected override ActiveEnergyTotalImportCalculationItemModel ToModel(
    ActiveEnergyTotalImportCalculationItemEntity entity)
  {
    return entity switch
    {
      UsageActiveEnergyTotalImportT0CalculationItemEntity t0Entity => t0Entity
        .ToModel(),
      UsageActiveEnergyTotalImportT1CalculationItemEntity t1Entity => t1Entity
        .ToModel(),
      UsageActiveEnergyTotalImportT2CalculationItemEntity t2Entity => t2Entity
        .ToModel(),
      SupplyActiveEnergyTotalImportT1CalculationItemEntity t1Entity => t1Entity
        .ToModel(),
      SupplyActiveEnergyTotalImportT2CalculationItemEntity t2Entity => t2Entity
        .ToModel(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }
}

public static class
  ActiveEnergyTotalImportCalculationItemModelEntityConverterExtensions
{
  public static UsageActiveEnergyTotalImportT0CalculationItemModel ToModel(
    this UsageActiveEnergyTotalImportT0CalculationItemEntity entity)
  {
    return new UsageActiveEnergyTotalImportT0CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static UsageActiveEnergyTotalImportT1CalculationItemModel ToModel(
    this UsageActiveEnergyTotalImportT1CalculationItemEntity entity)
  {
    return new UsageActiveEnergyTotalImportT1CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static UsageActiveEnergyTotalImportT2CalculationItemModel ToModel(
    this UsageActiveEnergyTotalImportT2CalculationItemEntity entity)
  {
    return new UsageActiveEnergyTotalImportT2CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static SupplyActiveEnergyTotalImportT1CalculationItemModel ToModel(
    this SupplyActiveEnergyTotalImportT1CalculationItemEntity entity)
  {
    return new SupplyActiveEnergyTotalImportT1CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static SupplyActiveEnergyTotalImportT2CalculationItemModel ToModel(
    this SupplyActiveEnergyTotalImportT2CalculationItemEntity entity)
  {
    return new SupplyActiveEnergyTotalImportT2CalculationItemModel
    {
      Min_Wh = entity.Min_Wh,
      Max_Wh = entity.Max_Wh,
      Amount_Wh = entity.Amount_Wh,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static UsageActiveEnergyTotalImportT0CalculationItemEntity ToEntity(
    this ActiveEnergyTotalImportT0CalculationItemModel model)
  {
    return new UsageActiveEnergyTotalImportT0CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static UsageActiveEnergyTotalImportT1CalculationItemEntity ToEntity(
    this ActiveEnergyTotalImportT1CalculationItemModel model)
  {
    return new UsageActiveEnergyTotalImportT1CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static UsageActiveEnergyTotalImportT2CalculationItemEntity ToEntity(
    this ActiveEnergyTotalImportT2CalculationItemModel model)
  {
    return new UsageActiveEnergyTotalImportT2CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static SupplyActiveEnergyTotalImportT1CalculationItemEntity ToEntity(
    this SupplyActiveEnergyTotalImportT1CalculationItemModel model)
  {
    return new SupplyActiveEnergyTotalImportT1CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static SupplyActiveEnergyTotalImportT2CalculationItemEntity ToEntity(
    this SupplyActiveEnergyTotalImportT2CalculationItemModel model)
  {
    return new SupplyActiveEnergyTotalImportT2CalculationItemEntity
    {
      Min_Wh = model.Min_Wh,
      Max_Wh = model.Max_Wh,
      Amount_Wh = model.Amount_Wh,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }
}
