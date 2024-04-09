using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class CalculationModelEntityConverter : ModelEntityConverter<
  CalculationModel, CalculationEntity>
{
  protected override CalculationEntity ToEntity(
    CalculationModel model)
  {
    return model.ToEntity();
  }

  protected override CalculationModel ToModel(
    CalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class CalculationModelEntityConverterExtensions
{
  public static CalculationEntity ToEntity(this CalculationModel model)
  {
    if (model is RedLowCalculationModel redLowCalculationModel)
    {
      return redLowCalculationModel.ToEntity();
    }

    if (model is BlueLowCalculationModel blueLowCalculationModel)
    {
      return blueLowCalculationModel.ToEntity();
    }

    if (model is WhiteLowCalculationModel whiteLowCalculationModel)
    {
      return whiteLowCalculationModel.ToEntity();
    }

    if (model is WhiteMediumCalculationModel whiteMediumCalculationModel)
    {
      return whiteMediumCalculationModel.ToEntity();
    }

    throw new NotSupportedException(
      $"CalculationModel type {model.GetType().Name} is not supported."
    );
  }

  public static CalculationModel ToModel(this CalculationEntity entity)
  {
    if (entity is RedLowCalculationEntity redLowCalculationEntity)
    {
      return redLowCalculationEntity.ToModel();
    }

    if (entity is BlueLowCalculationEntity blueLowCalculationEntity)
    {
      return blueLowCalculationEntity.ToModel();
    }

    if (entity is WhiteLowCalculationEntity whiteLowCalculationEntity)
    {
      return whiteLowCalculationEntity.ToModel();
    }

    if (entity is WhiteMediumCalculationEntity whiteMediumCalculationEntity)
    {
      return whiteMediumCalculationEntity.ToModel();
    }

    throw new NotSupportedException(
      $"CalculationEntity type {entity.GetType().Name} is not supported."
    );
  }
}

