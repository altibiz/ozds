using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Messaging.Entities.Base;

namespace Ozds.Business.Conversion.Implementations;

public class StateModelEntityConverter
  : ConcreteModelEntityConverter<StateModel, StateEntity>
{
  public override void InitializeEntity(
    StateModel model,
    StateEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.CurrentState = model.CurrentState;
  }

  public override void InitializeModel(
    StateEntity entity,
    StateModel model
  )
  {
    base.InitializeModel(entity, model);
    model.CurrentState = entity.CurrentState;
  }
}
