using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;
using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers;

public class NetworkUserInvoiceStateObserver
: INetworkUserInvoiceStatePublisher, INetworkUserInvoiceStateSubscriber
{
  private event EventHandler<NetworkUserInvoiceStateEventArgs>? OnRegistered;

  public void PublishRegistered(NetworkUserInvoiceState state)
  {
    OnRegistered?.Invoke(this, new NetworkUserInvoiceStateEventArgs { State = state });
  }

  public void Subscribe(EventHandler<NetworkUserInvoiceStateEventArgs> handler)
  {
    OnRegistered += handler;
  }

  public void Unsubscribe(EventHandler<NetworkUserInvoiceStateEventArgs> handler)
  {
    OnRegistered -= handler;
  }
}
