using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Ozds.Client.Attributes;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

public partial class NavigationDrawer : OzdsComponentBase
{
  [CascadingParameter]
  private LayoutState LayoutState { get; set; } = default!;

  [CascadingParameter]
  private UserState UserState { get; set; } = default!;

  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  private List<NavigationDescriptor> Descriptors { get; }
    = NavigationAttribute.GetNavigationDescriptors().ToList();

  protected override void OnInitialized()
  {
    NavigationManager.LocationChanged += OnLocationChanged;
  }

  protected override void Dispose(bool disposing)
  {
    if (IsDisposed)
    {
      return;
    }

    if (disposing)
    {
      NavigationManager.LocationChanged -= OnLocationChanged;
    }

    base.Dispose(disposing);
  }

  private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
  {
    LayoutState.SetNavigationDrawerOpen(false);
  }

  private void SetNavigationDrawerOpen(bool open)
  {
    LayoutState.SetNavigationDrawerOpen(open);
  }

  private void ToggleNavigationDrawer()
  {
    LayoutState.SetNavigationDrawerOpen(!LayoutState.IsNavigationDrawerOpen);
  }
}
