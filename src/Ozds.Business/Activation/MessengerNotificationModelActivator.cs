using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class MessengerNotificationModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
  MessengerNotificationModel,
  NotificationModel>(serviceProvider)
{
  public override void Initialize(MessengerNotificationModel model)
  {
    base.Initialize(model);
    model.MessengerId = "0";
  }
}
