using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Messaging;

public class NetworkUserInvoiceStateModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<NetworkUserInvoiceStateModel, StateModel>(
  serviceProvider
)
{
  public override void Initialize(NetworkUserInvoiceStateModel model)
  {
    base.Initialize(model);

    model.NetworkUserInvoiceId = "0";
  }
}
