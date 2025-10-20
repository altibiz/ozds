using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations;

public class JoinModelActivator : ConcreteModelActivator<JoinModel>
{
  public override void Initialize(JoinModel model)
  {
    base.Initialize(model);

    model.ActivationSide = model.LeftType;
  }
}
