using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserCatalogueModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  NetworkUserCatalogueModel,
  CatalogueModel,
  NetworkUserCatalogueEntity,
  CatalogueEntity>(serviceProvider)
{
  public override void InitializeEntity(
    NetworkUserCatalogueModel model,
    NetworkUserCatalogueEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterFeePrice_EUR = model.MeterFeePrice_EUR;
  }

  public override void InitializeModel(
    NetworkUserCatalogueEntity entity,
    NetworkUserCatalogueModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeterFeePrice_EUR = entity.MeterFeePrice_EUR;
  }
}
