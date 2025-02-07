using Ozds.Users.Mutations.Abstractions;
using Ozds.Users.Queries;
using Ozds.Users.Queries.Abstractions;

namespace Ozds.Users.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsUsers(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddQueries();
    services.AddMutations();
    return services;
  }

  private static IServiceCollection AddQueries(this IServiceCollection services)
  {
    services.AddScoped(typeof(IUserQueries), typeof(UserQueries));
    return services;
  }

  private static IServiceCollection AddMutations(
    this IServiceCollection services
  )
  {
    services.AddScoped(typeof(IMutations));
    return services;
  }
}
