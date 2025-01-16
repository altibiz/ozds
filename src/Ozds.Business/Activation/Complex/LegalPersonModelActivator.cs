using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class LegalPersonModelActivator
  : ConcreteModelActivator<LegalPersonModel>
{
  public override void Initialize(LegalPersonModel model)
  {
    model.Name = string.Empty;
    model.SocialSecurityNumber = string.Empty;
    model.Address = string.Empty;
    model.PostalCode = string.Empty;
    model.City = string.Empty;
    model.Email = string.Empty;
    model.PhoneNumber = string.Empty;
  }
}
