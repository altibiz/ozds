using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers.Abstractions;

public interface
  INetworkUserInvoiceStateSubscriber : ISubscriber<
  INetworkUserInvoiceStatePublisher>
{
  public void SubscribeRegistered(
    EventHandler<NetworkUserInvoiceStateEventArgs> handler);

  public void UnsubscribeRegistered(
    EventHandler<NetworkUserInvoiceStateEventArgs> handler);
}
