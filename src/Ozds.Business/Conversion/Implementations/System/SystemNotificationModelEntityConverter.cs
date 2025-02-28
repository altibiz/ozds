using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class SystemNotificationEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
  SystemNotificationModel,
  NotificationModel,
  SystemNotificationEntity,
  NotificationEntity>(serviceProvider)
{
}
