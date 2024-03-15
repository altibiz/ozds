using Microsoft.Extensions.DependencyInjection;
using Ozds.Client.State;

namespace Ozds.Client.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddCascadingMaybeRepresentingUserState(this IServiceCollection services)
  {
    services.AddCascadingAuthenticationState();
    services.AddCascadingValue((_) => default(UserState));
    services.AddCascadingValue((_) => default(RepresentativeState));
    return services;
  }
}
