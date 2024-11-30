using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.Pages;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class UserDrawer : OzdsComponentBase
{
  [CascadingParameter]
  private LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  private UserState UserState { get; set; } = default!;

  private void OnAccountClick()
  {
    LayoutState.SetUserDrawerOpen(false);
    NavigateToPage<AccountPage>();
  }

  private void OnLogoutClick()
  {
    LayoutState.SetUserDrawerOpen(false);
  }
}
