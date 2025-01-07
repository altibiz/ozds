namespace Ozds.Business.Observers.EventArgs;

public class JobsBillingJobEventArgs : System.EventArgs
{
  public required string NetworkUserId { get; init; }
}
