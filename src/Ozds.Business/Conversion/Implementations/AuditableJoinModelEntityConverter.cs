using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations;

public class AuditableJoinModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    AuditableJoinModel,
    JoinModel,
    AuditableJoinEntity,
    JoinEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    AuditableJoinModel model,
    AuditableJoinEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.CreatedOn = model.CreatedOn;
    entity.CreatedById = model.CreatedById;
  }

  public override void InitializeModel(
    AuditableJoinEntity entity,
    AuditableJoinModel model
  )
  {
    base.InitializeModel(entity, model);
    model.CreatedOn = entity.CreatedOn;
    model.CreatedById = entity.CreatedById;
  }
}
