using Altibiz.DependencyInjection.Extensions;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsAssets(
    this IServiceCollection services
  )
  {
    services.AddQueries();
    return services;
  }

  private static IServiceCollection AddQueries(
    this IServiceCollection services
  )
  {
    services.AddScopedAssignableTo(typeof(IQueries));
    return services;
  }
}
