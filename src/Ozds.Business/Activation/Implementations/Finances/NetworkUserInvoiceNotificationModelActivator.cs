using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class NetworkUserInvoiceNotificationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  NetworkUserInvoiceNotificationModel,
  NotificationModel>(serviceProvider)
{
  public override void Initialize(NetworkUserInvoiceNotificationModel model)
  {
    base.Initialize(model);
    model.InvoiceId = "0";
  }
}
