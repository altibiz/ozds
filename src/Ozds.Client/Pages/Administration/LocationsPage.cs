using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class LocationsPage : OzdsComponentBase
{
  private bool _deleted;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

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
