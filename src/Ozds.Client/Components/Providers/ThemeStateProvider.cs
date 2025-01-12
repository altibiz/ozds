using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class ThemeStateProvider : OzdsComponentBase
{
  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [Inject]
  private ILocalStorageService LocalStorageService { get; set; } = default!;

  private const string DarkModeKey = "darkMode";

  private ThemeState? _state;

  private MudThemeProvider _mudThemeProvider = default!;

  protected override async Task OnInitializedAsync()
  {
    var theme = ThemeState.DefaultTheme();

    var darkMode = await GetDarkModeFromLocalStorage();
    if (darkMode is null)
    {
      darkMode = await GetDarkModeFromSystem();
      await SetDarkModeToLocalStorage(darkMode.Value);
    }

    _state = new ThemeState(
      Theme: theme,
      IsDarkMode: darkMode.Value,
      SetTheme: async theme =>
      {
        _state = _state! with { Theme = theme };
        await InvokeAsync(StateHasChanged);
      },
      SetDarkMode: async isDarkMode =>
      {
        await SetDarkModeToLocalStorage(isDarkMode);
        _state = _state! with { IsDarkMode = isDarkMode };
        await InvokeAsync(StateHasChanged);
      }
    );
  }

  private async Task SetDarkModeToLocalStorage(bool isDarkMode)
  {
    await LocalStorageService.SetItemAsync(DarkModeKey, isDarkMode);
  }

  private async Task<bool?> GetDarkModeFromLocalStorage()
  {
    return await LocalStorageService.GetItemAsync<bool>(DarkModeKey);
  }

  private async Task<bool> GetDarkModeFromSystem()
  {
    return await _mudThemeProvider.GetSystemPreference();
  }
}
