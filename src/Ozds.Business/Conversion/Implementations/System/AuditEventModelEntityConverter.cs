using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Conversion.Implementations.System;

public class AuditEventModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  AuditEventModel,
  EventModel,
  AuditEventEntity,
  EventEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    AuditEventModel model,
    AuditEventEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.Audit = modelEntityConverter.ToEntity<AuditEntity>(model.Audit);
  }

  public override void InitializeModel(
    AuditEventEntity entity,
    AuditEventModel model)
  {
    base.InitializeModel(entity, model);
    model.Audit = modelEntityConverter.ToModel<AuditModel>(entity.Audit);
  }
}
