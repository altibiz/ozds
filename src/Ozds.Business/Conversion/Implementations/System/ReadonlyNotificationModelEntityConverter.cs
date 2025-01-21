using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.System;

public class ReadonlyNotificationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      ReadonlyNotificationModel,
      NotificationModel,
      ReadonlyNotificationEntity,
      NotificationEntity>(serviceProvider)
{
}
