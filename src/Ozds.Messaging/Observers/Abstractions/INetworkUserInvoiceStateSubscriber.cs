using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers.Abstractions;

public interface
  INetworkUserInvoiceStateSubscriber : ISubscriber<
  INetworkUserInvoiceStatePublisher, NetworkUserInvoiceStateEventArgs>
{
}
