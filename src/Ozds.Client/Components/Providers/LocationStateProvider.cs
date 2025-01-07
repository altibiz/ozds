using Blazored.LocalStorage;
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

  [Inject]
  private ILocalStorageService LocalStorageService { get; set; } = default!;

  private const string LocationKey = "location";

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

    var locationId = await GetLocationFromLocalStorage();

    var locationSet = locationId is not null;

    if (!locationSet
      && RepresentativeState.Representative.Role
        is RoleModel.NetworkUserRepresentative)
    {
      await SetLocationToLocalStorage(null);
      locationSet = true;
    }

    var locationQueries = ScopedServices
      .GetRequiredService<LocationQueries>();

    if (!locationSet)
    {
      var locations = await locationQueries.ReadAllLocationsByRepresentativeId(
        RepresentativeState.Representative.Id,
        RepresentativeState.Representative.Role,
        CancellationToken.None
      );

      _representativeLocations = locations;
      return;
    }

    LocationModel? location = null;
    if (locationId is { })
    {
      location = await locationQueries.ReadLocationByRepresentativeId(
        RepresentativeState.Representative.Id,
        RepresentativeState.Representative.Role,
        locationId,
        CancellationToken.None,
        deleted: false
      );
    }
    if (location is null)
    {
      return;
    }

    _state = new LocationState(
      location,
      () =>
      {
        InvokeAsync(async () =>
        {
          await SetLocationToLocalStorage(null);
          _state = null;
          StateHasChanged();
        });
      }
    );
  }

  private async Task OnLocationSelected(LocationModel location)
  {
    await SetLocationToLocalStorage(location.Id);
    _state = new LocationState(
      location,
      () =>
      {
        InvokeAsync(async () =>
        {
          await SetLocationToLocalStorage(null);
          _state = null;
          StateHasChanged();
        });
      }
    );
  }

  private async Task SetLocationToLocalStorage(string? location)
  {
    await LocalStorageService.SetItemAsync(LocationKey, location);
  }

  private async Task<string?> GetLocationFromLocalStorage()
  {
    return await LocalStorageService.GetItemAsync<string>(LocationKey);
  }
}
