using Microsoft.AspNetCore.Components;
using Ozds.Business.Models;
using Ozds.Business.Queries;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NetworkUserPage
  : OzdsIdentifiableModelPageComponentBase<NetworkUserModel>
{
  [Parameter]
  public string? Id { get; set; }

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
