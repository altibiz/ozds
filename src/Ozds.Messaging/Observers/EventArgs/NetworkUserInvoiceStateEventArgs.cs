using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers.EventArgs;

public class NetworkUserInvoiceStateEventArgs : System.EventArgs
{
  public required NetworkUserInvoiceState State { get; init; }
}
