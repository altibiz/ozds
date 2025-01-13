using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class UserToolbar : OzdsComponentBase
{
  [CascadingParameter]
  public LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  public UserState UserState { get; set; } = default!;

  [CascadingParameter]
  public ThemeState ThemeState { get; set; } = default!;

  [CascadingParameter]
  public CultureState CultureState { get; set; } = default!;

  [CascadingParameter]
  public LocationState? LocationState { get; set; } = default!;

  private string CultureName => CultureState.Culture.TwoLetterISOLanguageName;

  private string UserName => UserState.User.UserName;

  private bool IsDarkMode => ThemeState.IsDarkMode;

  private string DarkModeName => Translate(IsDarkMode ? "Light" : "Dark");

  private Task ExitLocation()
  {
    return LocationState?.UnsetLocation() ?? Task.CompletedTask;
  }

  private Task ToggleDarkMode()
  {
    return ThemeState.SetDarkMode(!ThemeState.IsDarkMode);
  }

  private void ToggleLocalizationDrawer()
  {
    LayoutState.SetLocalizationDrawerOpen(!LayoutState.IsLocalizationDrawerOpen);
  }

  private void ToggleUserDrawer()
  {
    LayoutState.SetUserDrawerOpen(!LayoutState.IsUserDrawerOpen);
  }
}
