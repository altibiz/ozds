using MudBlazor;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class PrintLayout : OzdsLayoutComponentBase
{
  private ThemeState ThemeState { get; set; } = default!;

#pragma warning disable S4487 // Unread "private" fields should be removed
#pragma warning disable IDE0052 // Remove unread private members
  private MudThemeProvider _mudThemeProvider = default!;
#pragma warning restore IDE0052 // Remove unread private members
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
    ThemeState = new ThemeState(
      Theme: ThemeState.Default(),
      IsDarkMode: false,
      SetTheme: theme =>
      {
        ThemeState = ThemeState with { Theme = theme };
        InvokeAsync(StateHasChanged);
      },
      SetDarkMode: isDarkMode =>
      {
        ThemeState = ThemeState with { IsDarkMode = isDarkMode };
        InvokeAsync(StateHasChanged);
      }
    );
  }
}
