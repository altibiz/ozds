using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation.Implementations.System;

public class SystemNotificationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  SystemNotificationModel,
  NotificationModel>(serviceProvider)
{
  public override void Initialize(SystemNotificationModel model)
  {
    base.Initialize(model);
    model.Topics = [TopicModel.All];
  }
}
