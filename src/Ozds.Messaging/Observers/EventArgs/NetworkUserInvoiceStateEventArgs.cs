using Ozds.Messaging.Entities;

namespace Ozds.Messaging.Observers.EventArgs;

public class NetworkUserInvoiceStateEventArgs : System.EventArgs
{
  public required NetworkUserInvoiceStateEntity State { get; init; }
}
