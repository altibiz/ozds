namespace Ozds.Jobs.Observers.EventArgs;

public class MessengerInactivityEventArgs : System.EventArgs
{
  public required string Id { get; init; }
}
