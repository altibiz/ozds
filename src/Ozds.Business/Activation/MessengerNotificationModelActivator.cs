using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class MessengerNotificationModelActivator
  : ModelActivator<MessengerNotificationModel>
{
  public override MessengerNotificationModel ActivateConcrete()
  {
    return New();
  }

  public static MessengerNotificationModel New()
  {
    return new MessengerNotificationModel
    {
      Id = default!,
      Title = string.Empty,
      Timestamp = DateTimeOffset.UtcNow,
      Content = string.Empty,
      Summary = string.Empty,
      Topics = new HashSet<TopicModel>(),
      ResolvedOn = default!,
      ResolvedById = default!,
      MessengerId = default!
    };
  }
}
