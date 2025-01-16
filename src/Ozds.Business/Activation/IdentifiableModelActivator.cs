using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class IdentifiableModelActivator
  : ConcreteModelActivator<IdentifiableModel>
{
  public override void Initialize(IdentifiableModel model)
  {
    base.Initialize(model);
    model.Id = default!;
    model.Title = string.Empty;
  }
}
