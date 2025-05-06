using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Report.Entities;

namespace Ozds.Business.Conversion.Implementations.Report;

public class NetworkUserMeasurementLocationModelReportEntityConverter(
  IServiceProvider serviceProvider)
  : InheritingModelReportEntityConverter<
    NetworkUserMeasurementLocationModel,
    IdentifiableModel,
    NetworkUserMeasurementLocationEntity,
    IdentifiableEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    NetworkUserMeasurementLocationModel model,
    NetworkUserMeasurementLocationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterId = model.MeterId;
    entity.NetworkUserId = model.NetworkUserId;
    entity.NetworkUserCatalogueId = model.NetworkUserCatalogueId;
  }

  public override void InitializeModel(
    NetworkUserMeasurementLocationEntity entity,
    NetworkUserMeasurementLocationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.MeterId = entity.MeterId;
    model.NetworkUserId = entity.NetworkUserId;
    model.NetworkUserCatalogueId = entity.NetworkUserCatalogueId;
  }
}
