using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public abstract class ReadonlyNotificationModel : NotificationModel, IReadonly
{
}
