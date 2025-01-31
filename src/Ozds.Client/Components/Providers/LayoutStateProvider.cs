using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class LayoutStateProvider : ComponentBase
{
  private LayoutState? _state;

  [Parameter]
  public RenderFragment? ChildContent { get; set; }

  protected override void OnInitialized()
  {
    _state = new LayoutState(
      false,
      false,
      false,
      isUserDrawerOpen =>
      {
        InvokeAsync(
          () =>
          {
            _state = _state! with { IsUserDrawerOpen = isUserDrawerOpen };
            StateHasChanged();
          });
      },
      isLocalizationDrawerOpen =>
      {
        InvokeAsync(
          () =>
          {
            _state = _state! with
            {
              IsLocalizationDrawerOpen = isLocalizationDrawerOpen
            };
            StateHasChanged();
          });
      },
      isNavigationDrawerOpen =>
      {
        InvokeAsync(
          () =>
          {
            _state = _state! with
            {
              IsNavigationDrawerOpen = isNavigationDrawerOpen
            };
            StateHasChanged();
          });
      },
      Breakpoint.None,
      breakpoint =>
      {
        InvokeAsync(
          () =>
          {
            _state = _state! with { Breakpoint = breakpoint };
            StateHasChanged();
          });
      }
    );
  }
}
