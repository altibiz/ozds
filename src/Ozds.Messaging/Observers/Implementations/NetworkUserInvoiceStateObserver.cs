using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.Base;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Messaging.Observers.Implementations;

public class NetworkUserInvoiceStateObserver
  : Observer<NetworkUserInvoiceStateEventArgs>,
    INetworkUserInvoiceStatePublisher,
    INetworkUserInvoiceStateSubscriber
{
}
