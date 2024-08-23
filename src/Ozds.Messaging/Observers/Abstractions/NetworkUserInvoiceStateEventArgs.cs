using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers.Abstractions;

public class NetworkUserInvoiceStateEventArgs : EventArgs
{
  public required NetworkUserInvoiceState State { get; init; }
}
