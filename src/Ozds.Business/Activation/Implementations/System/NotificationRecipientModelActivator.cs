using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Activation.Implementations.System;

public class NotificationRecipientModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<NotificationRecipientModel, JoinModel>(
  serviceProvider
)
{
  public override void Initialize(NotificationRecipientModel model)
  {
    base.Initialize(model);

    model.NotificationId = "0";
    model.RepresentativeId = string.Empty;
  }
}
