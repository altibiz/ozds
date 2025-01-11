using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class LayoutStateProvider : ComponentBase
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
          InvokeAsync(() =>
          {
            _state = _state! with { IsUserDrawerOpen = isUserDrawerOpen };
            StateHasChanged();
          });
        },
        SetLocalizationDrawerOpen: isLocalizationDrawerOpen =>
        {
          InvokeAsync(() =>
          {
            _state = _state! with { IsLocalizationDrawerOpen = isLocalizationDrawerOpen };
            StateHasChanged();
          });
        },
        SetNavigationDrawerOpen: isNavigationDrawerOpen =>
        {
          InvokeAsync(() =>
          {
            _state = _state! with { IsNavigationDrawerOpen = isNavigationDrawerOpen };
            StateHasChanged();
          });
        },
        Breakpoint: Breakpoint.None,
        SetBreakpoint: breakpoint =>
        {
          InvokeAsync(() =>
          {
            _state = _state! with { Breakpoint = breakpoint };
            StateHasChanged();
          });
        }
    );
  }
}
