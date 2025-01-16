using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class ResolvableNotificationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<ResolvableNotificationModel, NotificationModel>(
      serviceProvider
    )
{
  public override void Initialize(ResolvableNotificationModel model)
  {
    base.Initialize(model);

    model.ResolvedById = default!;
    model.ResolvedOn = default!;
  }
}
