using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Activation;

public class NetworkUserInvoiceNotificationModelActivator
  : ModelActivator<NetworkUserInvoiceNotificationModel>
{
  public override NetworkUserInvoiceNotificationModel ActivateConcrete()
  {
    return New();
  }

  public static NetworkUserInvoiceNotificationModel New()
  {
    return new NetworkUserInvoiceNotificationModel
    {
      Id = default!,
      Title = string.Empty,
      Timestamp = DateTimeOffset.UtcNow,
      Content = string.Empty,
      Summary = string.Empty,
      Topics = new HashSet<TopicModel>(),
      InvoiceId = default!
    };
  }
}
