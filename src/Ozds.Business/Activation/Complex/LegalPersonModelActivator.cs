using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class LegalPersonModelActivator : ModelActivator<LegalPersonModel>
{
  public override LegalPersonModel ActivateConcrete()
  {
    return new LegalPersonModel
    {
      Name = string.Empty,
      SocialSecurityNumber = string.Empty,
      Address = string.Empty,
      PostalCode = string.Empty,
      City = string.Empty,
      Email = string.Empty,
      PhoneNumber = string.Empty
    };
  }

  public static LegalPersonModel New()
  {
    return new LegalPersonModel
    {
      Name = string.Empty,
      SocialSecurityNumber = string.Empty,
      Address = string.Empty,
      PostalCode = string.Empty,
      City = string.Empty,
      Email = string.Empty,
      PhoneNumber = string.Empty
    };
  }
}
