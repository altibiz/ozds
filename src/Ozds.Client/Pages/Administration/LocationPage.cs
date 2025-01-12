using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Agnostic;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class LocationPage : OzdsOwningComponentBase
{
  [Parameter]
  public string? Id { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<LocationModel?> OnLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    var queries = ScopedServices.GetRequiredService<LocationQueries>();

    var location = await queries.ReadLocationByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      Id,
      CancellationToken
    );

    return location;
  }
}
