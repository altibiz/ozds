namespace Ozds.Client.State;

public record LayoutState(
  bool IsUserDrawerOpen,
  bool IsLocalizationDrawerOpen,
  bool IsNavigationDrawerOpen,
  Action<bool> SetUserDrawerOpen,
  Action<bool> SetLocalizationDrawerOpen,
  Action<bool> SetNavigationDrawerOpen
);
