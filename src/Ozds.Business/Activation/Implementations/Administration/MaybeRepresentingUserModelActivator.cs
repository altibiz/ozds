using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Activation.Implementations.Administration;

public class MaybeRepresentingUserModelActivator(
  IServiceProvider serviceProvider
) : ConcreteModelActivator<MaybeRepresentingUserModel>
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(MaybeRepresentingUserModel model)
  {
    model.User = modelActivator.Activate<UserModel>();
    model.NewPassword = modelActivator.Activate<NewPasswordModel>();
    model.Representative = modelActivator.Activate<RepresentativeModel>();
  }
}
