using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Implementations.Administration;

public class NewPasswordModelActivator : ConcreteModelActivator<NewPasswordModel>
{
  public override void Initialize(NewPasswordModel model)
  {
    base.Initialize(model);

    model.UserId = string.Empty;
    model.NewPassword = string.Empty;
    model.ConfirmNewPassword = string.Empty;
  }
}
