using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Base;

public class DisposableComponentBase : ComponentBase, IDisposable
{
  private static readonly CancellationTokenSource _cancelledTokenSource =
    CreateCancelledTokenSource();

  private CancellationTokenSource? cancellationTokenSource = new();

  protected CancellationToken CancellationToken =>
    cancellationTokenSource?.Token ?? _cancelledTokenSource.Token;

  protected bool IsDisposed { get; private set; }

  private static CancellationTokenSource CreateCancelledTokenSource()
  {
    var cancellationTokenSource = new CancellationTokenSource();
    cancellationTokenSource.Cancel();
    return cancellationTokenSource;
  }

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
#pragma warning disable S1066 // Mergeable "if" statements should be combined
      if (cancellationTokenSource is not null)
#pragma warning restore S1066 // Mergeable "if" statements should be combined
      {
        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
        cancellationTokenSource = null;
      }
    }

    IsDisposed = true;
  }
}
