using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations;

public class AuditableModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
  AuditableModel,
  IdentifiableModel,
  AuditableEntity,
  IdentifiableEntity>(serviceProvider)
{
  public override void InitializeEntity(
    AuditableModel model,
    AuditableEntity entity)
  {
    base.InitializeEntity(model, entity);
    entity.CreatedOn = model.CreatedOn;
    entity.CreatedById = model.CreatedById;
    entity.LastUpdatedOn = model.LastUpdatedOn;
    entity.LastUpdatedById = model.LastUpdatedById;
    entity.IsDeleted = model.IsDeleted;
    entity.DeletedOn = model.DeletedOn;
    entity.DeletedById = model.DeletedById;
  }

  public override void InitializeModel(
    AuditableEntity entity,
    AuditableModel model)
  {
    base.InitializeModel(entity, model);
    model.CreatedOn = entity.CreatedOn;
    model.CreatedById = entity.CreatedById;
    model.LastUpdatedOn = entity.LastUpdatedOn;
    model.LastUpdatedById = entity.LastUpdatedById;
    model.IsDeleted = entity.IsDeleted;
    model.DeletedOn = entity.DeletedOn;
    model.DeletedById = entity.DeletedById;
  }
}
