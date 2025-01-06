using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.Base;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers;

public class NetworkUserInvoiceStateObserver
  : Observer<NetworkUserInvoiceStateEventArgs>,
  INetworkUserInvoiceStatePublisher,
  INetworkUserInvoiceStateSubscriber
{
}
