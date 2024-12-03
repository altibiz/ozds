using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class LocationStateProvider : OzdsOwningComponentBase
{
  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private string? _previousRepresentativeId;

  private LocationState? _state;

  private List<LocationModel> _representativeLocations = new();

  private string _filter = "";

  protected override async Task OnParametersSetAsync()
  {
    if (_previousRepresentativeId == RepresentativeState.Representative.Id)
    {
      return;
    }
    _previousRepresentativeId = RepresentativeState.Representative.Id;

    if (RepresentativeState.Representative.Role
      is RoleModel.NetworkUserRepresentative)
    {
      _state = new(default, default);
      return;
    }

    var locationQueries = ScopedServices
      .GetRequiredService<LocationQueries>();

    var locations = await locationQueries.ReadAllLocationsByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      CancellationToken.None
    );

    _representativeLocations = locations;
  }

  private void OnLocationSelected(LocationModel location)
  {
    _state = new LocationState(
      location,
      () =>
      {
        _state = default;
        InvokeAsync(StateHasChanged);
      }
    );
  }
}
