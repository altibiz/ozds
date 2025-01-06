namespace Ozds.Jobs.Observers.EventArgs;

public class BillingJobEventArgs : System.EventArgs
{
  public required string NetworkUserId { get; init; }
}
