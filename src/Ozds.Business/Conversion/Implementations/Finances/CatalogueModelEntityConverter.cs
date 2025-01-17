using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class CatalogueModelEntityConverter : ConcreteModelEntityConverter<
  ICatalogue, CatalogueEntity>
{
  protected override CatalogueEntity ToEntity(ICatalogue model)
  {
    return model.ToEntity();
  }

  protected override ICatalogue ToModel(CatalogueEntity entity)
  {
    return entity.ToModel();
  }
}

public static class CatalogueModelEntityConverterExtensions
{
  public static CatalogueEntity ToEntity(this ICatalogue model)
  {
    if (model is NetworkUserCatalogueModel
      networkUserCatalogueModel)
    {
      return networkUserCatalogueModel.ToEntity();
    }

    if (model is RegulatoryCatalogueModel
      regulatoryCatalogueModel)
    {
      return regulatoryCatalogueModel.ToEntity();
    }

    throw new NotSupportedException(
      $"CatalogueModel type {model.GetType().Name} is not supported."
    );
  }

  public static ICatalogue ToModel(this CatalogueEntity entity)
  {
    if (entity is NetworkUserCatalogueEntity
      networkUserCatalogueEntity)
    {
      return networkUserCatalogueEntity.ToModel();
    }

    if (entity is RegulatoryCatalogueEntity
      regulatoryCatalogueEntity)
    {
      return regulatoryCatalogueEntity.ToModel();
    }

    throw new NotSupportedException(
      $"CatalogueEntity type {entity.GetType().Name} is not supported."
    );
  }
}
