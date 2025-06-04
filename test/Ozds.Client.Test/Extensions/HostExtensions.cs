using Altibiz.DependencyInjection.Extensions;
using Ozds.Client.Test.Faking;
using Ozds.Client.Test.Faking.Abstractions;

namespace Ozds.Client.Test.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsClientTest(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddFaking();
    return builder;
  }

  public static IHostApplicationBuilder AddFaking(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransientAssignableTo<IModelFaker>();
    builder.Services.AddSingleton<ModelFaker>();
    return builder;
  }
}
