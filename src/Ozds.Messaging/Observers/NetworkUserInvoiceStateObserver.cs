using Ozds.Messaging.Entities;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers;

public class NetworkUserInvoiceStateObserver
  : INetworkUserInvoiceStatePublisher, INetworkUserInvoiceStateSubscriber
{
  public void PublishRegistered(NetworkUserInvoiceStateEntity state)
  {
    OnRegistered?.Invoke(
      this, new NetworkUserInvoiceStateEventArgs { State = state });
  }

  public void SubscribeRegistered(
    EventHandler<NetworkUserInvoiceStateEventArgs> handler)
  {
    OnRegistered += handler;
  }

  public void UnsubscribeRegistered(
    EventHandler<NetworkUserInvoiceStateEventArgs> handler)
  {
    OnRegistered -= handler;
  }

  private event EventHandler<NetworkUserInvoiceStateEventArgs>? OnRegistered;
}
