using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Administration;

public class PhysicalPersonModelActivator
  : ConcreteModelActivator<PhysicalPersonModel>
{
  public override void Initialize(PhysicalPersonModel model)
  {
    model.Name = string.Empty;
    model.Email = string.Empty;
    model.PhoneNumber = string.Empty;
  }
}
