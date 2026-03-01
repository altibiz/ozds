using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Activation.Implementations.Administration;

public class MaybeRepresentingUserModelActivator
  (IServiceProvider serviceProvider)
  : ConcreteModelActivator<MaybeRepresentingUserModel>
{

  private readonly ModelActivator modelActivator =
  serviceProvider.GetRequiredService<ModelActivator>();

  override public void Initialize(MaybeRepresentingUserModel model)
  {
    model.User = modelActivator.Activate<UserModel>();
    model.Representative = modelActivator.Activate<RepresentativeModel>();
  }
}
