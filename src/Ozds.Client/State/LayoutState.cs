using MudBlazor;

namespace Ozds.Client.State;

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
  public Anchor DrawerAnchor
  {
    get
    {
      return Breakpoint <= Breakpoint.Sm
        ? Anchor.Bottom
        : Anchor.Top;
    }
  }
}
