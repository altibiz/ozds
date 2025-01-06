using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers.Abstractions;

public interface
  INetworkUserInvoiceStatePublisher : IPublisher<
  INetworkUserInvoiceStateSubscriber, NetworkUserInvoiceStateEventArgs>
{
}
