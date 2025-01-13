using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.Pages;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class RepresentativeToolbar : OzdsComponentBase
{
  [CascadingParameter]
  public LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  public NotificationsState? NotificationsState { get; set; } = default!;

  private void OnNavigationMenuClick()
  {
    LayoutState.SetNavigationDrawerOpen(true);
  }

  private void NavigateToNotifications()
  {
    NavigateToPage<NotificationsPage>();
  }
}
