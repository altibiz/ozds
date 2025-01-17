using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations;

public class IdentifiableModelActivator
  : ConcreteModelActivator<IdentifiableModel>
{
  public override void Initialize(IdentifiableModel model)
  {
    base.Initialize(model);
    model.Id = "0";
    model.Title = string.Empty;
  }
}
