using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Ozds.Client.Base;

#pragma warning disable S3881
public abstract class OzdsOwningComponentBase : OzdsComponentBase, IDisposable
{
  private AsyncServiceScope? _scope;

  [Inject]
  private IServiceScopeFactory ScopeFactory { get; set; } = default!;

  protected bool IsDisposed { get; private set; }

  protected IServiceProvider ScopedServices
  {
    get
    {
      if (ScopeFactory == null)
      {
        throw new InvalidOperationException(
          "Services cannot be accessed before the component is initialized."
        );
      }

      ObjectDisposedException.ThrowIf(IsDisposed, this);
      if (!_scope.HasValue)
      {
        _scope = ScopeFactory.CreateAsyncScope();
      }

      return _scope.Value.ServiceProvider;
    }
  }

#pragma warning disable CA1816
  void IDisposable.Dispose()
  {
    if (!IsDisposed)
    {
      _scope?.Dispose();
      _scope = null;
      Dispose(true);
      IsDisposed = true;
    }
  }
#pragma warning restore CA1816

#pragma warning disable S2953
  protected virtual void Dispose(bool disposing)
  {
  }
#pragma warning restore S2953
}
#pragma warning restore S3881
