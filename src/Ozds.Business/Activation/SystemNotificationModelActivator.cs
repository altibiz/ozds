using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class SystemNotificationModelActivator
  : ModelActivator<SystemNotificationModel>
{
  public override SystemNotificationModel ActivateConcrete()
  {
    return New();
  }

  public static SystemNotificationModel New()
  {
    return new SystemNotificationModel
    {
      Id = default!,
      Title = string.Empty,
      Timestamp = DateTimeOffset.UtcNow,
      Content = string.Empty,
      Summary = string.Empty,
      Topics = new HashSet<TopicModel>()
    };
  }
}
