using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class ResolvableNotificationModel : NotificationModel
{

  public string? ResolvedById { get; set; } = default!;

  public required DateTimeOffset? ResolvedOn { get; set; } = default!;
}
