using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ozds.Client.Attributes;
using Ozds.Client.State;
using Ozds.Data;
using Ozds.Data.Models;

namespace Ozds.Client.Shared.Layout;

public partial class MainLayout
{
  private LoadingState<RepresentativeState> _representativeState = new();

  private LoadingState<UserState> _userState = new();

  [Inject] private NavigationManager NavigationManager { get; set; } = default!;

  [Inject] private ILogger<MainLayout> Logger { get; set; } = default!;

  [Inject] private ServiceProvider Services { get; set; } = default!;

  public static IEnumerable<NavigationDescriptor> GetNavigationDescriptors()
  {
    foreach (var type in typeof(App).Assembly.GetTypes())
    {
      if (type.GetCustomAttribute(typeof(RouteAttribute)) is RouteAttribute
            routeAttribute
          && type.GetCustomAttribute(typeof(NavigationAttribute)) is
            NavigationAttribute navigationAttribute)
      {
        if (navigationAttribute.Title is { })
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
      RedirectTo("/login");
      return;
    }

    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId is null)
    {
      _userState =
        _userState.WithError("Claims principal does not contain a user id.");
      return;
    }

    var maybeRepresentingUser = await ReadMaybeRepresentingUser(userId);
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
    string userId)
  {
    await using var scope = Services.CreateAsyncScope();
    var client = scope.ServiceProvider.GetRequiredService<OzdsDbClient>();
    return await client.ReadMaybeRepresentingUserByUser(userId);
  }

  private void RedirectTo(string route)
  {
    try
    {
      NavigationManager.NavigateTo(route, true);
    }
    catch (Exception exception)
    {
      Logger.LogError(
        "Error redirecting to {}: {}",
        route,
        exception.Message
      );
    }
  }

  public record NavigationDescriptor(string Title, string Route);
}
