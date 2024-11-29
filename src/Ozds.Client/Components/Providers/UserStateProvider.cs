using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class UserStateProvider : OzdsOwningComponentBase
{
  [CascadingParameter]
  private Task<AuthenticationState>? AuthenticationStateTask { get; set; }

  [Parameter]
  public string LogoutToken { get; set; } = default!;

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  private async Task<UserState?> LoadAsync()
  {
    if (AuthenticationStateTask is null)
    {
      return default;
    }

    var authenticationState = await AuthenticationStateTask;
    var claimsPrincipal = authenticationState?.User;
    if (!(claimsPrincipal?.Identity?.IsAuthenticated ?? false))
    {
      return default;
    }

    var representativeQueries = ScopedServices
      .GetRequiredService<RepresentativeQueries>();

    var user = await representativeQueries
      .UserByClaimsPrincipal(
        claimsPrincipal,
        CancellationToken.None);
    if (user is null)
    {
      return default;
    }

    var state = new UserState(
      claimsPrincipal,
      LogoutToken,
      user
    );

    return state;
  }
}
