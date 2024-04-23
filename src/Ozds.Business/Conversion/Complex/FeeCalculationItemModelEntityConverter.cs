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
      _ => throw new InvalidOperationException("Unknown tariff type")
    };
  }

  protected override FeeCalculationItemModel ToModel(
    FeeCalculationItemEntity entity)
  {
    return entity switch
    {
      UsageMeterFeeCalculationItemEntity usageEntity => usageEntity.ToModel(),
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
}
