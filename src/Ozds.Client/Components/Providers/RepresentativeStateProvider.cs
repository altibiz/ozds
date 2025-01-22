using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class RepresentativeStateProvider : OzdsComponentBase
{
  [Parameter]
  public RenderFragment Found { get; set; } = default!;

  [Parameter]
  public RenderFragment NotFound { get; set; } = default!;

  [CascadingParameter]
  private UserState UserState { get; set; } = default!;

  [Inject]
  private IServiceScopeFactory ServiceScopeFactory { get; set; } = default!;

  private async Task<RepresentativeState?> LoadAsync()
  {
    var representativeQueries = ScopedServices
      .GetRequiredService<RepresentativeQueries>();

    var representative = await representativeQueries
      .ReadRepresentativeByUserId(
        UserState.User.Id,
        CancellationToken);
    if (representative is null)
    {
      return default;
    }

    var state = new RepresentativeState(representative);

    return state;
  }
}
