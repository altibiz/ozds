using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Migration.Arguments;
using Ozds.Migration.Extensions;
using Ozds.Report.Extensions;
using Ozds.Time.Extensions;
using Ozds.Users.Extensions;

namespace Ozds.Migration.Hosting;

public sealed class OzdsMigrationHost : IHost
{
  private readonly IHost inner;

  public OzdsMigrationHost(IOzdsMigrationArguments arguments)
  {
    var builder = Host.CreateApplicationBuilder();

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
      .AddOzdsMigration(arguments);

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
