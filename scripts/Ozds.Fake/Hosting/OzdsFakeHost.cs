using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Fake.Arguments;
using Ozds.Fake.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Report.Extensions;
using Ozds.Time.Extensions;
using Ozds.Users.Extensions;

namespace Ozds.Fake.Hosting;

public sealed class OzdsFakeHost : IHost
{
  private readonly IHost inner;

  public OzdsFakeHost(
    IOzdsFakeArguments arguments,
    HostApplicationBuilderSettings? settings = null,
    Action<IHostApplicationBuilder>? configure = null
  )
  {
    var builder = settings is null
      ? Host.CreateApplicationBuilder()
      : Host.CreateApplicationBuilder(settings);

    if (configure is not null)
    {
      configure(builder);
    }

    builder.Configuration.AddConfiguration(
      new ConfigurationBuilder()
        .AddInMemoryCollection(
          new Dictionary<string, string?>
          {
            { "Ozds:Users:WithAuth", "false" },
            { "Ozds:Messaging:WithBus", "false" },
            { "Ozds:Business:WithReactors", "false" }
          })
        .Build());

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
      .AddOzdsFake(arguments);

    inner = builder.Build();
  }

  public IServiceProvider Services
  {
    get { return inner.Services; }
  }

  public Task StartAsync(CancellationToken cancellationToken = default)
  {
    return inner.StartAsync(cancellationToken);
  }

  public Task StopAsync(CancellationToken cancellationToken = default)
  {
    return inner.StopAsync(cancellationToken);
  }

  public void Dispose()
  {
    inner.Dispose();
  }
}
