using Ozds.Business.Hosting;
using Ozds.Client.Extensions;
using Ozds.Server.Extensions;

namespace Ozds.Server.Hosting;

public sealed class OzdsServerHost(
  string[] arguments,
  Action<IHostApplicationBuilder>? configure = null
) : OzdsBusinessHost<WebApplicationBuilder, WebApplication>(
  WebApplication.CreateBuilder(arguments),
  builder =>
  {
    if (configure is not null)
    {
      configure(builder);
    }
  },
  builder =>
  {
    builder
      .AddOzdsClient()
      .AddOzdsServer();

    // FIXME: Altibiz.DependencyInjection.Extensions adds proxy types
    // from proxy assemblies
    var proxies = builder.Services
      .Where(service => service.ServiceType.Name.EndsWith("Proxy"))
      .ToList();
    foreach (var service in proxies)
    {
      Console.WriteLine($"Removing proxy: {service.ServiceType.Name}");
      builder.Services.Remove(service);
    }

    var app = builder.Build();

    app.UseOzdsServer();

    return app;
  }
)
{
}
