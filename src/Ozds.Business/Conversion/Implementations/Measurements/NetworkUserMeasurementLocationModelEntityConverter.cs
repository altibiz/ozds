using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Measurements;

public class NetworkUserMeasurementLocationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
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
  }

  public override void InitializeModel(
    NetworkUserMeasurementLocationEntity entity,
    NetworkUserMeasurementLocationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.NetworkUserId = entity.NetworkUserId;
    model.NetworkUserCatalogueId = entity.NetworkUserCatalogueId;
  }
}
