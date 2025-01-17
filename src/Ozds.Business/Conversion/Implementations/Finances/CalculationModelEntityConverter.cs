using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CalculationModelEntityConverter : ConcreteModelEntityConverter<
  ICalculation, CalculationEntity>
{
  protected override CalculationEntity ToEntity(
    ICalculation model)
  {
    return model.ToEntity();
  }

  protected override ICalculation ToModel(
    CalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class CalculationModelEntityConverterExtensions
{
  public static CalculationEntity ToEntity(
    this ICalculation model)
  {
    if (model is NetworkUserCalculationModel
      networkUserLowCalculationModel)
    {
      return networkUserLowCalculationModel.ToEntity();
    }

    throw new NotSupportedException(
      $"CalculationModel type {model.GetType().Name} is not supported."
    );
  }

  public static CalculationModel ToModel(
    this CalculationEntity entity)
  {
    if (entity is NetworkUserCalculationEntity
      networkUserCalculationEntity)
    {
      return networkUserCalculationEntity.ToModel();
    }

    throw new NotSupportedException(
      $"CalculationEntity type {entity.GetType().Name} is not supported."
    );
  }
}
