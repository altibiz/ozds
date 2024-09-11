using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Complex;

public class PhysicalPersonModelActivator : ModelActivator<PhysicalPersonModel>
{
  public override PhysicalPersonModel ActivateConcrete()
  {
    return New();
  }

  public static PhysicalPersonModel New()
  {
    return new PhysicalPersonModel
    {
      Email = string.Empty,
      PhoneNumber = string.Empty,
      Name = string.Empty
    };
  }

  public static PhysicalPersonModel New(UserModel user)
  {
    return new PhysicalPersonModel
    {
      Name = string.Empty,
      Email = user.Email,
      PhoneNumber = string.Empty
    };
  }
}
