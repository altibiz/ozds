using Altibiz.DependencyInjection.Extensions;
using Ozds.Iot.Observers.Abstractions;

namespace Ozds.Iot.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsIot(
    this IServiceCollection services
  )
  {
    services.AddObservers();
    return services;
  }

  private static IServiceCollection AddObservers(
    this IServiceCollection services
  )
  {
    services.AddSingletonAssignableTo(typeof(IPublisher));
    services.AddSingletonAssignableTo(typeof(ISubscriber));
    return services;
  }
}
