using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Client.Attributes;
using Ozds.Client.State;
using Ozds.Data;
using Ozds.Data.Models;

namespace Ozds.Client.Shared.Layout;

public partial class MainLayout : LayoutComponentBase
{
  private LoadingState<RepresentativeState> _representativeState = new();

  private LoadingState<UserState> _userState = new();

  [Inject] private NavigationManager NavigationManager { get; set; } = default!;

  [Inject] private IServiceProvider Services { get; set; } = default!;

  public MainLayout(IHttpContextAccessor httpContextAccessor) : base()
  {
    Console.WriteLine(httpContextAccessor.HttpContext?.User.Identity?.Name);
  }

  public static IEnumerable<NavigationDescriptor> GetNavigationDescriptors()
  {
    foreach (var type in typeof(App).Assembly.GetTypes())
    {
      if (type.GetCustomAttribute(typeof(RouteAttribute)) is RouteAttribute
            routeAttribute
          && type.GetCustomAttribute(typeof(NavigationAttribute)) is
            NavigationAttribute navigationAttribute)
      {
        if (navigationAttribute.Title is not null)
        {
          yield return new NavigationDescriptor(
            navigationAttribute.Title,
            routeAttribute.Template
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
    var claimsPrincipal = authenticationState?.User;
    if (claimsPrincipal is null)
    {
      throw new InvalidOperationException("No claims principal found.");
    }
    if (claimsPrincipal.Identity?.IsAuthenticated is false)
    {
      NavigationManager.NavigateTo("/login?returnUrl=/client/pages/dashboard");
      return;
    }

    var maybeRepresentingUser = await ReadMaybeRepresentingUser(claimsPrincipal);
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
    var client = scope.ServiceProvider.GetRequiredService<OzdsDbClient>();
    return await client.ReadMaybeRepresentingUserByClaimsPrincipal(claimsPrincipal);
  }

  public record NavigationDescriptor(string Title, string Route);
}
