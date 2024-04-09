using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class CatalogueModelEntityConverter : ModelEntityConverter<
  CatalogueModel, CatalogueEntity>
{
  protected override CatalogueEntity ToEntity(
    CatalogueModel model)
  {
    return model.ToEntity();
  }

  protected override CatalogueModel ToModel(
    CatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class CatalogueModelEntityConverterExtensions
{
  public static CatalogueEntity ToEntity(this CatalogueModel model)
  {
    if (model is RedLowCatalogueModel redLowCatalogueModel)
    {
      return redLowCatalogueModel.ToEntity();
    }

    if (model is BlueLowCatalogueModel blueLowCatalogueModel)
    {
      return blueLowCatalogueModel.ToEntity();
    }

    if (model is WhiteLowCatalogueModel whiteLowCatalogueModel)
    {
      return whiteLowCatalogueModel.ToEntity();
    }

    if (model is WhiteMediumCatalogueModel whiteMediumCatalogueModel)
    {
      return whiteMediumCatalogueModel.ToEntity();
    }

    throw new NotSupportedException(
      $"CatalogueModel type {model.GetType().Name} is not supported."
    );
  }

  public static CatalogueModel ToModel(this CatalogueEntity entity)
  {
    if (entity is RedLowCatalogueEntity redLowCatalogueEntity)
    {
      return redLowCatalogueEntity.ToModel();
    }

    if (entity is BlueLowCatalogueEntity blueLowCatalogueEntity)
    {
      return blueLowCatalogueEntity.ToModel();
    }

    if (entity is WhiteLowCatalogueEntity whiteLowCatalogueEntity)
    {
      return whiteLowCatalogueEntity.ToModel();
    }

    if (entity is WhiteMediumCatalogueEntity whiteMediumCatalogueEntity)
    {
      return whiteMediumCatalogueEntity.ToModel();
    }

    throw new NotSupportedException(
      $"CatalogueEntity type {entity.GetType().Name} is not supported."
    );
  }
}

