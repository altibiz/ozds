using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IMessagingNetworkUserInvoiceStateSubscriber
  : ISubscriber<MessagingNetworkUserInvoiceStateEventArgs>
{
}
