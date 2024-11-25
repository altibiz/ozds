using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public partial class NavigationDrawer : OzdsComponentBase, IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
  [CascadingParameter]
  public LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  public UserState UserState { get; set; } = default!;

  protected override void OnInitialized()
  {
    NavigationManager.LocationChanged += OnLocationChanged;
  }

  public void Dispose()
  {
    NavigationManager.LocationChanged -= OnLocationChanged;
  }

  private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
  {
    LayoutState.SetNavigationDrawerOpen(false);
  }
}
