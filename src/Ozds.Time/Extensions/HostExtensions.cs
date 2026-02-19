using Altibiz.DependencyInjection.Extensions;
using Ozds.Time.Clock;
using Ozds.Time.Options;
using Ozds.Time.Queries.Abstractions;

namespace Ozds.Time.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsTime(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddServices();
    builder.AddQueries();
    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsTimeOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddServices(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingleton<ClockWinder>();
    builder.Services.AddHostedService(services =>
      services.GetRequiredService<ClockWinder>()
    );
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo<IQueries>();
    return builder;
  }
}
