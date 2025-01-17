using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserCatalogueModelEntityConverter : ConcreteModelEntityConverter<
  INetworkUserCatalogue, NetworkUserCatalogueEntity>
{
  protected override NetworkUserCatalogueEntity ToEntity(
    INetworkUserCatalogue model)
  {
    return model.ToEntity();
  }

  protected override INetworkUserCatalogue ToModel(
    NetworkUserCatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NetworkUserCatalogueModelEntityConverterExtensions
{
  public static NetworkUserCatalogueEntity ToEntity(
    this INetworkUserCatalogue model)
  {
    if (model is RedLowNetworkUserCatalogueModel
      redLowNetworkUserCatalogueModel)
    {
      return redLowNetworkUserCatalogueModel.ToEntity();
    }

    if (model is BlueLowNetworkUserCatalogueModel
      blueLowNetworkUserCatalogueModel)
    {
      return blueLowNetworkUserCatalogueModel.ToEntity();
    }

    if (model is WhiteLowNetworkUserCatalogueModel
      whiteLowNetworkUserCatalogueModel)
    {
      return whiteLowNetworkUserCatalogueModel.ToEntity();
    }

    if (model is WhiteMediumNetworkUserCatalogueModel
      whiteMediumNetworkUserCatalogueModel)
    {
      return whiteMediumNetworkUserCatalogueModel.ToEntity();
    }

    throw new NotSupportedException(
      $"NetworkUserCatalogueModel type {model.GetType().Name} is not supported."
    );
  }

  public static NetworkUserCatalogueModel ToModel(
    this NetworkUserCatalogueEntity entity)
  {
    if (entity is RedLowNetworkUserCatalogueEntity
      redLowNetworkUserCatalogueEntity)
    {
      return redLowNetworkUserCatalogueEntity.ToModel();
    }

    if (entity is BlueLowNetworkUserCatalogueEntity
      blueLowNetworkUserCatalogueEntity)
    {
      return blueLowNetworkUserCatalogueEntity.ToModel();
    }

    if (entity is WhiteLowNetworkUserCatalogueEntity
      whiteLowNetworkUserCatalogueEntity)
    {
      return whiteLowNetworkUserCatalogueEntity.ToModel();
    }

    if (entity is WhiteMediumNetworkUserCatalogueEntity
      whiteMediumNetworkUserCatalogueEntity)
    {
      return whiteMediumNetworkUserCatalogueEntity.ToModel();
    }

    throw new NotSupportedException(
      $"NetworkUserCatalogueEntity type {entity.GetType().Name} is not supported."
    );
  }
}
