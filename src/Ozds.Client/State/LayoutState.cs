namespace Ozds.Client.State;
using MudBlazor;

public record LayoutState(
    bool IsUserDrawerOpen,
    bool IsLocalizationDrawerOpen,
    bool IsNavigationDrawerOpen,
    Action<bool> SetUserDrawerOpen,
    Action<bool> SetLocalizationDrawerOpen,
    Action<bool> SetNavigationDrawerOpen,
    Breakpoint Breakpoint,
    Action<Breakpoint> SetBreakpoint
)
{
  public Anchor DrawerAnchor => (Breakpoint <= Breakpoint.Sm)
    ? Anchor.Bottom
    : Anchor.Top;
}
