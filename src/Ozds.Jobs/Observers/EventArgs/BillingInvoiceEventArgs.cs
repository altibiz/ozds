namespace Ozds.Jobs.Observers.EventArgs;

public class NetworkUserInvoiceEventArgs : System.EventArgs
{
  public string NetworkUserId { get; set; } = default!;
}
