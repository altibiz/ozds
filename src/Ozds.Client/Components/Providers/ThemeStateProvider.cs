using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

// FIXME: detecting dark mode preference is broken

namespace Ozds.Client.Components.Providers;

public partial class ThemeStateProvider : OzdsComponentBase
{
  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  private ThemeState? _state;

#pragma warning disable S4487 // Unread "private" fields should be removed
  private MudThemeProvider _mudThemeProvider = default!;
#pragma warning restore S4487 // Unread "private" fields should be removed

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
  protected override async Task OnAfterRenderAsync(bool firstRender)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
  {
    if (firstRender)
    {
#pragma warning disable S125 // Sections of code should not be commented out
      // ThemeState.SetDarkMode(
      //   await _mudThemeProvider?.GetSystemPreference());
      // StateHasChanged();
#pragma warning restore S125 // Sections of code should not be commented out
    }
  }

  protected override void OnInitialized()
  {
    _state = new ThemeState(
      Theme: ThemeState.Default(),
      IsDarkMode: false,
      SetTheme: theme =>
      {
        _state = _state! with { Theme = theme };
        InvokeAsync(StateHasChanged);
      },
      SetDarkMode: isDarkMode =>
      {
        _state = _state! with { IsDarkMode = isDarkMode };
        InvokeAsync(StateHasChanged);
      }
    );
  }
}
