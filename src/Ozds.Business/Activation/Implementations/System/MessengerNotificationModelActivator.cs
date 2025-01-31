using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class MessengerNotificationModelActivator(
  IServiceProvider serviceProvider)
  : InheritingModelActivator<
    MessengerNotificationModel,
    NotificationModel>(serviceProvider)
{
  public override void Initialize(MessengerNotificationModel model)
  {
    base.Initialize(model);
    model.MessengerId = string.Empty;
  }
}
