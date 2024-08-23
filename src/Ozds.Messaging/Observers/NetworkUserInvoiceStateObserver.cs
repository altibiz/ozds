using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers;

public class NetworkUserInvoiceStateObserver
: INetworkUserInvoiceStatePublisher, INetworkUserInvoiceStateSubscriber
{
  public event EventHandler<NetworkUserInvoiceStateEventArgs>? OnRegistered;

  public void PublishRegistered(NetworkUserInvoiceState state)
  {
    OnRegistered?.Invoke(this, new NetworkUserInvoiceStateEventArgs { State = state });
  }
}
