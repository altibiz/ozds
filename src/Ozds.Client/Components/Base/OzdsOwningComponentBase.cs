using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Ozds.Client.Components.Base;

public abstract class OzdsOwningComponentBase : OzdsComponentBase
{
  private AsyncServiceScope? _scope;

  [Inject]
  private IServiceScopeFactory ScopeFactory { get; set; } = default!;

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

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      _scope?.Dispose();
    }
    base.Dispose(disposing);
  }
}
