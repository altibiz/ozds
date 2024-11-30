using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class LayoutStateProvider
{
  [Parameter]
  public RenderFragment? ChildContent { get; set; }

  private LayoutState? _state;

  protected override void OnInitialized()
  {
    _state = new LayoutState(
        IsUserDrawerOpen: false,
        IsLocalizationDrawerOpen: false,
        IsNavigationDrawerOpen: false,
        SetUserDrawerOpen: isUserDrawerOpen =>
        {
          _state = _state! with { IsUserDrawerOpen = isUserDrawerOpen };
          InvokeAsync(StateHasChanged);
        },
        SetLocalizationDrawerOpen: isLocalizationDrawerOpen =>
        {
          _state = _state! with { IsLocalizationDrawerOpen = isLocalizationDrawerOpen };
          InvokeAsync(StateHasChanged);
        },
        SetNavigationDrawerOpen: isNavigationDrawerOpen =>
        {
          _state = _state! with { IsNavigationDrawerOpen = isNavigationDrawerOpen };
          InvokeAsync(StateHasChanged);
        },
        Breakpoint: Breakpoint.None,
        SetBreakpoint: breakpoint =>
        {
          _state = _state! with { Breakpoint = breakpoint };
          InvokeAsync(StateHasChanged);
        }
    );
  }
}
