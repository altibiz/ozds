using Altibiz.DependencyInjection.Extensions;
using Ozds.Iot.Observers.Abstractions;

namespace Ozds.Iot.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsIot(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddObservers();
    return builder;
  }

  private static IHostApplicationBuilder AddObservers(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IPublisher));
    builder.Services.AddSingletonAssignableTo(typeof(ISubscriber));
    return builder;
  }
}
