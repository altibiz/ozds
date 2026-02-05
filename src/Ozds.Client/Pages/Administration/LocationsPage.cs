using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class LocationsPage : OzdsComponentBase
{
  private bool deleted;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<PaginatedList<LocationModel>> OnPageAsync(
    string search,
    int page,
    int pageCount
  )
  {
    var queries = ScopedServices.GetRequiredService<LocationQueries>();

    var locations = await queries.ReadIndirectByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      page,
      CancellationToken,
      deleted: deleted,
      title: search
    );

    return locations;
  }
}
