namespace Ozds.Business.Models.Base;

public class ResolvableNotificationModel : NotificationModel
{

  public string? ResolvedById { get; set; } = default!;

  public required DateTimeOffset? ResolvedOn { get; set; } = default!;
}
