using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class ReadonlyNotificationActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<
    ReadonlyNotificationModel,
    NotificationModel>(serviceProvider)
{
}
