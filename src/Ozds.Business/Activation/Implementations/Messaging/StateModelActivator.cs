using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Messaging;

public class StateModelActivator : ConcreteModelActivator<StateModel>
{
  public override void Initialize(StateModel model)
  {
    base.Initialize(model);

    model.CurrentState = string.Empty;
  }
}
