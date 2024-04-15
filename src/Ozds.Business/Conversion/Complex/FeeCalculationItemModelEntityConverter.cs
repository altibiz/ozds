using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Complex;

namespace Ozds.Business.Conversion.Complex;

public class FeeCalculationItemModelEntityConverter : ModelEntityConverter<
  FeeCalculationItemModel, FeeCalculationItemEntity>
{
  protected override FeeCalculationItemEntity ToEntity(
    FeeCalculationItemModel model)
  {
    return model switch
    {
      UsageMeterFeeCalculationItemModel usageModel => usageModel.ToEntity(),
      SupplyBusinessUsageFeeCalculationItemModel supplyBusinessModel =>
        supplyBusinessModel.ToEntity(),
      SupplyRenewableEnergyFeeCalculationItemModel supplyRenewableModel =>
        supplyRenewableModel.ToEntity(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }

  protected override FeeCalculationItemModel ToModel(
    FeeCalculationItemEntity entity)
  {
    return entity switch
    {
      UsageMeterFeeCalculationItemEntity usageEntity => usageEntity.ToModel(),
      SupplyBusinessUsageFeeCalculationItemEntity supplyBusinessEntity =>
        supplyBusinessEntity.ToModel(),
      SupplyRenewableEnergyFeeCalculationItemEntity supplyRenewableEntity =>
        supplyRenewableEntity.ToModel(),
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }
}

public static class FeeCalculationItemModelEntityConverterExtensions
{
  public static UsageMeterFeeCalculationItemModel ToModel(
    this UsageMeterFeeCalculationItemEntity entity)
  {
    return new UsageMeterFeeCalculationItemModel
    {
      Amount_N = entity.Amount_N,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static SupplyBusinessUsageFeeCalculationItemModel ToModel(
    this SupplyBusinessUsageFeeCalculationItemEntity entity)
  {
    return new SupplyBusinessUsageFeeCalculationItemModel
    {
      Amount_N = entity.Amount_N,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static SupplyRenewableEnergyFeeCalculationItemModel ToModel(
    this SupplyRenewableEnergyFeeCalculationItemEntity entity)
  {
    return new SupplyRenewableEnergyFeeCalculationItemModel
    {
      Amount_N = entity.Amount_N,
      Price_EUR = entity.Price_EUR,
      Total_EUR = entity.Total_EUR
    };
  }

  public static UsageMeterFeeCalculationItemEntity ToEntity(
    this UsageMeterFeeCalculationItemModel model)
  {
    return new UsageMeterFeeCalculationItemEntity
    {
      Amount_N = model.Amount_N,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static SupplyBusinessUsageFeeCalculationItemEntity ToEntity(
    this SupplyBusinessUsageFeeCalculationItemModel model)
  {
    return new SupplyBusinessUsageFeeCalculationItemEntity
    {
      Amount_N = model.Amount_N,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }

  public static SupplyRenewableEnergyFeeCalculationItemEntity ToEntity(
    this SupplyRenewableEnergyFeeCalculationItemModel model)
  {
    return new SupplyRenewableEnergyFeeCalculationItemEntity
    {
      Amount_N = model.Amount_N,
      Price_EUR = model.Price_EUR,
      Total_EUR = model.Total_EUR
    };
  }
}
