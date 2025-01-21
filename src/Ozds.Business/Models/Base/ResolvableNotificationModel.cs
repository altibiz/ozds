using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models.Base;

public class ResolvableNotificationModel
  : NotificationModel, IResolvableNotification
{
  public string? ResolvedById { get; set; } = default!;

  public required DateTimeOffset? ResolvedOn { get; set; } = default!;
}
