using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Client.Attributes;
using Ozds.Client.Base;
using Ozds.Client.State;

// TODO: remove hardcoding of /app here

namespace Ozds.Client.Shared.Layout;

public partial class MainLayout : OzdsLayoutComponentBase
{
  private LoadingState<RepresentativeState> _representativeState = new();

  private LoadingState<UserState> _userState = new();

  [Inject] private NavigationManager NavigationManager { get; set; } = default!;

  [Inject] private IServiceProvider Services { get; set; } = default!;

  public static IEnumerable<NavigationDescriptor> GetNavigationDescriptors(RepresentativeState rep)
  {
    foreach (var type in typeof(App).Assembly.GetTypes())
    {
      if (type.GetCustomAttribute(typeof(RouteAttribute)) is RouteAttribute
            routeAttribute
          && type.GetCustomAttribute(typeof(NavigationAttribute)) is
            NavigationAttribute navigationAttribute)
      {
        if (navigationAttribute.Title is not null)
        // && rep is not null
        // && navigationAttribute.Allows.Contains(rep.Representative.Role))
        {
          yield return new NavigationDescriptor(
            navigationAttribute.Title,
            "/app" + routeAttribute.Template
          );
        }
      }
    }
  }

  protected override async Task OnInitializedAsync()
  {
    if (_authenticationStateTask is null)
    {
      return;
    }

    var authenticationState = await _authenticationStateTask;
    var claimsPrincipal = authenticationState?.User ??
                          throw new InvalidOperationException(
                            "No claims principal found.");
    if (claimsPrincipal.Identity?.IsAuthenticated is false)
    {
      NavigationManager.NavigateTo("/login?returnUrl=/app");
      return;
    }

    var maybeRepresentingUser =
      await ReadMaybeRepresentingUser(claimsPrincipal);
    if (maybeRepresentingUser is null)
    {
      _userState = _userState.NotFound();
      return;
    }

    _userState =
      _userState.WithValue(new UserState(maybeRepresentingUser.User));
    if (maybeRepresentingUser.Representative is null)
    {
      _representativeState = _representativeState.NotFound();
      return;
    }

    _representativeState = _representativeState.WithValue(
      new RepresentativeState(maybeRepresentingUser.Representative));
  }

  private async Task<MaybeRepresentingUserModel?> ReadMaybeRepresentingUser(
    ClaimsPrincipal claimsPrincipal)
  {
    await using var scope = Services.CreateAsyncScope();
    var query =
      scope.ServiceProvider.GetRequiredService<OzdsRepresentativeQueries>();
    return await query.MaybeRepresentingUserByClaimsPrincipal(claimsPrincipal);
  }

  public record NavigationDescriptor(string Title, string Route);
}
// TODO: remove hardcoding of /app here
