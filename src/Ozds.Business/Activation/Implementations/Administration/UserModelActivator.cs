using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Implementations.Administration;

public class UserModelActivator : ConcreteModelActivator<UserModel>
{
  public override void Initialize(UserModel model)
  {
    base.Initialize(model);

    model.Id = string.Empty;
    model.Name = string.Empty;
    model.Email = string.Empty;
  }
}
