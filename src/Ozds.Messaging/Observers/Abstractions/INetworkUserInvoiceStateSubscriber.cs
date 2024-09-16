using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers.Abstractions;

public interface INetworkUserInvoiceStateSubscriber : ISubscriber<INetworkUserInvoiceStatePublisher>
{
  public void Subscribe(EventHandler<NetworkUserInvoiceStateEventArgs> handler);

  public void Unsubscribe(
    EventHandler<NetworkUserInvoiceStateEventArgs> handler);
}
