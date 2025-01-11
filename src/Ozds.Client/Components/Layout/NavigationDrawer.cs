using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Ozds.Client.Attributes;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Layout;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public partial class NavigationDrawer : OzdsComponentBase, IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
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

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
  public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
  {
    NavigationManager.LocationChanged -= OnLocationChanged;
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
