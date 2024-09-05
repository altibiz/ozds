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
    services.AddScoped<IUserQueries, UserQueries>();

    return services;
  }

  public static IApplicationBuilder UseOzdsUsers(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
