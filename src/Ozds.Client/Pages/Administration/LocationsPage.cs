using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class LocationsPage : OzdsComponentBase
{
  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private bool _deleted = false;

  private async Task<PaginatedList<LocationModel>> OnPageAsync(int page)
  {
    var queries = ScopedServices.GetRequiredService<LocationQueries>();

    var locations = await queries.ReadLocationsByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      page,
      CancellationToken,
      deleted: _deleted
    );

    return locations;
  }
}
