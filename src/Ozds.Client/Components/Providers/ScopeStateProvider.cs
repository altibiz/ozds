using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

// NOTE: rendered at root so keep dependencies minimal
public partial class ScopeStateProvider : DisposableComponentBase
{
  private IServiceScope? _scope;

  private ScopeState? _state;

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [Inject]
  private IServiceScopeFactory ScopeFactory { get; set; } = default!;

  protected override void OnParametersSet()
  {
    if (_scope is null)
    {
      _scope = ScopeFactory.CreateScope();
      _state = new ScopeState(_scope.ServiceProvider);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
#pragma warning disable S1066 // Mergeable "if" statements should be combined
      if (_scope is not null)
#pragma warning restore S1066 // Mergeable "if" statements should be combined
      {
        _state = null;
        _scope.Dispose();
        _scope = null;
      }
    }

    base.Dispose(disposing);
  }
}
