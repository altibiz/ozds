using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

// FIXME: detecting dark mode preference is broken

namespace Ozds.Client.Components.Layout;

public partial class Dev : OzdsLayoutComponentBase
{
  [CascadingParameter]
  private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

  [Inject]
  private IServiceScopeFactory ServiceScopeFactory { get; set; } = default!;

  private LayoutState LayoutState { get; set; } = default!;

  private ThemeState ThemeState { get; set; } = default!;

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
    LayoutState = new LayoutState(
        IsUserDrawerOpen: false,
        IsLocalizationDrawerOpen: false,
        IsNavigationDrawerOpen: false,
        SetUserDrawerOpen: isUserDrawerOpen =>
        {
          LayoutState = LayoutState with { IsUserDrawerOpen = isUserDrawerOpen };
          InvokeAsync(StateHasChanged);
        },
        SetLocalizationDrawerOpen: isLocalizationDrawerOpen =>
        {
          LayoutState = LayoutState with { IsLocalizationDrawerOpen = isLocalizationDrawerOpen };
          InvokeAsync(StateHasChanged);
        },
        SetNavigationDrawerOpen: isNavigationDrawerOpen =>
        {
          LayoutState = LayoutState with { IsNavigationDrawerOpen = isNavigationDrawerOpen };
          InvokeAsync(StateHasChanged);
        },
        Breakpoint: Breakpoint.None,
        SetBreakpoint: breakpoint =>
        {
          LayoutState = LayoutState with { Breakpoint = breakpoint };
          InvokeAsync(StateHasChanged);
        }
    );

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

  private async Task<(UserState? UserState, RepresentativeState? RepresentativeState)> LoadAsync()
  {
    if (AuthenticationStateTask is null)
    {
      return default;
    }

    var authenticationState = await AuthenticationStateTask;
    var claimsPrincipal = authenticationState?.User ??
      throw new InvalidOperationException(
        "No claims principal found.");
    if (!(claimsPrincipal.Identity?.IsAuthenticated ?? false))
    {
      NavigateToLogin();
      return default;
    }

    await using var scope = ServiceScopeFactory.CreateAsyncScope();
    var maybeRepresentingUser = await scope.ServiceProvider
      .GetRequiredService<RepresentativeQueries>()
      .MaybeRepresentingUserByClaimsPrincipal(
        claimsPrincipal,
        CancellationToken.None);
    if (maybeRepresentingUser is null)
    {
      NavigateToLogin();
      return default;
    }

    var userState = new UserState(claimsPrincipal, maybeRepresentingUser.User);
    if (maybeRepresentingUser.Representative is not { } representative)
    {
      return (userState, null);
    }

    var representativeState =
      new RepresentativeState(
        representative,
        new(),
        new(),
        (notification) =>
        {
        },
        (notification) =>
        {
        }
      );
    return (userState, representativeState);
  }

  private void OnBreakpointChanged(Breakpoint breakpoint)
  {
    LayoutState.SetBreakpoint(breakpoint);
  }
}