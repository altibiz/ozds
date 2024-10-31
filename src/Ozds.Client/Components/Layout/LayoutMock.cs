using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Ozds.Business.Models.Base;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Base;
using Ozds.Client.State;

// FIXME: detecting dark mode preference is broken

namespace Ozds.Client.Components.LayoutMock;

public partial class LayoutMock : OzdsLayoutComponentBase
{
  private ThemeState ThemeState { get; set; } = default!;

#pragma warning disable S4487 // Unread "private" fields should be removed
  private MudThemeProvider? _mudThemeProvider;

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
