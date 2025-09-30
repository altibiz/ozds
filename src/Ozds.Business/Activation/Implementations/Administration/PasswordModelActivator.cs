using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Implementations.Administration;

public class PasswordModelActivator : ConcreteModelActivator<PasswordModel>
{
  public override void Initialize(PasswordModel model)
  {
    base.Initialize(model);

    model.UserId = string.Empty;
    model.OldPassword = string.Empty;
    model.NewPassword = string.Empty;
    model.ConfirmNewPassword = string.Empty;
  }
}
