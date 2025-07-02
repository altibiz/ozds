using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Client.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Report.Extensions;
using Ozds.Server.Extensions;
using Ozds.Time.Extensions;
using Ozds.Users.Extensions;

namespace Ozds.Server.Hosting;

public sealed class OzdsServerHost : IHost
{
  private readonly IHost inner;

  public OzdsServerHost(
    string[] arguments,
    Action<IHostApplicationBuilder>? configure = null
  )
  {
    var builder = WebApplication.CreateBuilder(arguments);

    if (configure is not null)
    {
      configure(builder);
    }

    builder
      .AddOzdsTime()
      .AddOzdsAssets()
      .AddOzdsDocument()
      .AddOzdsReport()
      .AddOzdsUsers()
      .AddOzdsData()
      .AddOzdsMessaging()
      .AddOzdsJobs()
      .AddOzdsEmail()
      .AddOzdsBusiness()
      .AddOzdsIot()
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

    inner = app;
  }

  public IServiceProvider Services
  {
    get { return inner.Services; }
  }

  public void Dispose()
  {
    inner.Dispose();
  }

  public Task StartAsync(CancellationToken cancellationToken = default)
  {
    return inner.StartAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken = default)
  {
    return inner.StopAsync(cancellationToken);
  }
}
