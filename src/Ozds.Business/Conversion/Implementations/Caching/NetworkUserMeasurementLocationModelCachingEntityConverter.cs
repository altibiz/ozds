using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Caching;

public class NetworkUserMeasurementLocationModelCachingEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelCachingEntityConverter<
  NetworkUserMeasurementLocationModel,
  MeasurementLocationModel,
  NetworkUserMeasurementLocationEntity,
  MeasurementLocationEntity>(serviceProvider)
{
  public override void InitializeEntity(
    NetworkUserMeasurementLocationModel model,
    NetworkUserMeasurementLocationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.NetworkUserId = model.NetworkUserId;
    entity.NetworkUserCatalogueId = model.NetworkUserCatalogueId;
    entity.CalculationRemark = model.CalculationRemark;
  }

  public override void InitializeModel(
    NetworkUserMeasurementLocationEntity entity,
    NetworkUserMeasurementLocationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.NetworkUserId = entity.NetworkUserId;
    model.NetworkUserCatalogueId = entity.NetworkUserCatalogueId;
    model.CalculationRemark = entity.CalculationRemark;
  }
}
