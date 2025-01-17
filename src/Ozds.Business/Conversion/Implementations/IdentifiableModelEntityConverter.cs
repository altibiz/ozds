using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations;

public class IdentifiableModelEntityConverter
  : ConcreteModelEntityConverter<IdentifiableModel, IdentifiableEntity>
{
  public override void InitializeEntity(
    IdentifiableModel model,
    IdentifiableEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.Id = model.Id;
    entity.Title = model.Title;
  }

  public override void InitializeModel(
    IdentifiableEntity entity,
    IdentifiableModel model)
  {
    base.InitializeModel(entity, model);
    model.Id = entity.Id;
    model.Title = entity.Title;
  }
}
