namespace Ozds.Server.Test.Extensions;

public static class CancellationExtensions
{
  public static CancellationTokenSource CancelIn(
    this TimeSpan timeSpan,
    CancellationToken cancellationToken = default
  )
  {
    var cts = new CancellationTokenSource();
    cancellationToken.Register(cts.Cancel);
    Task.Run(
      async () =>
      {
        await Task.Delay(timeSpan, cts.Token);
        await cts.CancelAsync();
      },
      cancellationToken
    );
    return cts;
  }
}
