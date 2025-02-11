using Microsoft.AspNetCore.Components;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class UserStateProvider : OzdsComponentBase
{
  [Parameter]
  public string LogoutToken { get; set; } = default!;

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  private async Task<UserState?> LoadAsync()
  {
    var representativeQueries = ScopedServices
      .GetRequiredService<RepresentativeQueries>();

    var representativeId = await representativeQueries
      .ReadAuthenticatedRepresentativeId(CancellationToken);
    if (representativeId is null)
    {
      return default;
    }

    var user = await representativeQueries
      .ReadUserByUserId(
        representativeId,
        CancellationToken);
    if (user is null)
    {
      return default;
    }

    var state = new UserState(
      LogoutToken,
      user
    );

    return state;
  }
}
