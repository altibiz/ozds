using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NetworkUserPage : OzdsComponentBase
{
  [Parameter]
  public string? Id { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private async Task<NetworkUserModel?> OnLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    var queries = ScopedServices.GetRequiredService<NetworkUserQueries>();

    var networkUser = await queries.ReadNetworkUserByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      Id,
      CancellationToken
    );

    return networkUser;
  }
}
