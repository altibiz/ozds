namespace Ozds.Business.Observers.EventArgs;

public class ErrorEventArgs : System.EventArgs
{
  public string Message { get; set; } = default!;

  public Exception Exception { get; set; } = default!;
}
