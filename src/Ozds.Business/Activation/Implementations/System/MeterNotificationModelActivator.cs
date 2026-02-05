using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.System;

public class MeterNotificationModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<MeterNotificationModel, NotificationModel>(
    serviceProvider
  )
{
  public override void Initialize(MeterNotificationModel model)
  {
    base.Initialize(model);
    model.MeterId = string.Empty;
  }
}
