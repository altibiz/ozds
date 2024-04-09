using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class NetworkUserCalculationModelEntityConverter : ModelEntityConverter<
  NetworkUserCalculationModel, NetworkUserCalculationEntity>
{
  protected override NetworkUserCalculationEntity ToEntity(
    NetworkUserCalculationModel model)
  {
    return model.ToEntity();
  }

  protected override NetworkUserCalculationModel ToModel(
    NetworkUserCalculationEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NetworkUserCalculationModelEntityConverterExtensions
{
  public static NetworkUserCalculationEntity ToEntity(
    this NetworkUserCalculationModel model)
  {
    if (model is RedLowNetworkUserCalculationModel
        redLowNetworkUserCalculationModel)
    {
      return redLowNetworkUserCalculationModel.ToEntity();
    }

    if (model is BlueLowNetworkUserCalculationModel
        blueLowNetworkUserCalculationModel)
    {
      return blueLowNetworkUserCalculationModel.ToEntity();
    }

    if (model is WhiteLowNetworkUserCalculationModel
        whiteLowNetworkUserCalculationModel)
    {
      return whiteLowNetworkUserCalculationModel.ToEntity();
    }

    if (model is WhiteMediumNetworkUserCalculationModel
        whiteMediumNetworkUserCalculationModel)
    {
      return whiteMediumNetworkUserCalculationModel.ToEntity();
    }

    throw new NotSupportedException(
      $"NetworkUserCalculationModel type {model.GetType().Name} is not supported."
    );
  }

  public static NetworkUserCalculationModel ToModel(
    this NetworkUserCalculationEntity entity)
  {
    if (entity is RedLowNetworkUserCalculationEntity
        redLowNetworkUserCalculationEntity)
    {
      return redLowNetworkUserCalculationEntity.ToModel();
    }

    if (entity is BlueLowNetworkUserCalculationEntity
        blueLowNetworkUserCalculationEntity)
    {
      return blueLowNetworkUserCalculationEntity.ToModel();
    }

    if (entity is WhiteLowNetworkUserCalculationEntity
        whiteLowNetworkUserCalculationEntity)
    {
      return whiteLowNetworkUserCalculationEntity.ToModel();
    }

    if (entity is WhiteMediumNetworkUserCalculationEntity
        whiteMediumNetworkUserCalculationEntity)
    {
      return whiteMediumNetworkUserCalculationEntity.ToModel();
    }

    throw new NotSupportedException(
      $"NetworkUserCalculationEntity type {entity.GetType().Name} is not supported."
    );
  }
}
