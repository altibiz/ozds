using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class StateModelActivator : ConcreteModelActivator<StateModel>
{
  public override void Initialize(StateModel model)
  {
    base.Initialize(model);

    model.CurrentState = default!;
  }
}
