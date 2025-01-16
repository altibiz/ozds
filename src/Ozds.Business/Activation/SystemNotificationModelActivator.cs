using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class SystemNotificationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  SystemNotificationModel,
  NotificationModel>(serviceProvider)
{
}
