using Ozds.Business.Models;

namespace Ozds.Business.Observers.EventArgs;

public class MessagingNetworkUserInvoiceStateEventArgs : System.EventArgs
{
  public required NetworkUserInvoiceStateModel State { get; init; }
}
