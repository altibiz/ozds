using Microsoft.AspNetCore.Components;
using Ozds.Client.State;

namespace Ozds.Client.Components.Base;

// NOTE: to be used inside ErrorBoundary, ScopeStateProvider, TimeStateProvider
// and CultureStateProvider
public abstract partial class OzdsComponentBase : DisposableComponentBase
{
  [CascadingParameter]
  private ScopeState ScopeState { get; set; } = default!;

  protected IServiceProvider ScopedServices
  {
    get
    {
      return ScopeState?.ScopedServices ??
        throw new InvalidOperationException($"{this} got disposed");
    }
  }
}
