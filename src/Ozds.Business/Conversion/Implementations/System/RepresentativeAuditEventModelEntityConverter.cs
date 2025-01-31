using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class RepresentativeAuditEventEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  RepresentativeAuditEventModel,
  AuditEventModel,
  RepresentativeAuditEventEntity,
  AuditEventEntity>(serviceProvider)
{
  public override void InitializeEntity(
    RepresentativeAuditEventModel model,
    RepresentativeAuditEventEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.RepresentativeId = model.RepresentativeId;
  }

  public override void InitializeModel(
    RepresentativeAuditEventEntity entity,
    RepresentativeAuditEventModel model)
  {
    base.InitializeModel(entity, model);
    model.RepresentativeId = entity.RepresentativeId;
  }
}
