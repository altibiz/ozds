using Altibiz.DependencyInjection.Extensions;
using Ozds.Assets.Queries.Abstractions;

namespace Ozds.Assets.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsAssets(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddQueries();
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSingletonAssignableTo(typeof(IQueries));
    return builder;
  }
}
