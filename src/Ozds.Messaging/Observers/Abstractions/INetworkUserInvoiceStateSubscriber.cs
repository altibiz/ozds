using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers.Abstractions;

public interface INetworkUserInvoiceStateSubscriber
{
  public event EventHandler<NetworkUserInvoiceStateEventArgs>? OnRegistered;
}
