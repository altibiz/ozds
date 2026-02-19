using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations;

public class TrackableModelEntityConverter(IServiceProvider serviceProvider)
  : InheritingModelEntityConverter<
    TrackableModel,
    IdentifiableModel,
    TrackableEntity,
    IdentifiableEntity
  >(serviceProvider)
{
  public override void InitializeEntity(
    TrackableModel model,
    TrackableEntity entity
  )
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
    TrackableEntity entity,
    TrackableModel model
  )
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
