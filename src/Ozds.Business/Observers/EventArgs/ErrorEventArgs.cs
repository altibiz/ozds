namespace Ozds.Business.Observers.EventArgs;

public class ErrorEventArgs : System.EventArgs
{
  public required string Message { get; init; }

  public required Exception Exception { get; init; }
}
