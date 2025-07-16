using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Report.Extensions;
using Ozds.Time.Extensions;
using Ozds.Translation.Arguments;
using Ozds.Translation.Extensions;
using Ozds.Users.Extensions;

namespace Ozds.Translation.Hosting;

public sealed class OzdsTranslationHost : IHost
{
  private readonly IHost inner;

  public OzdsTranslationHost(IOzdsTranslationArguments arguments)
  {
    var builder = Host.CreateApplicationBuilder();

    builder.Configuration.AddInMemoryCollection(
      new Dictionary<string, string?>
      {
        { "Ozds:Users:WithAuth", "false" },
        { "Ozds:Messaging:WithBus", "false" },
        { "Ozds:Messaging:WithServices", "false" },
        { "Ozds:Jobs:WithServices", "false" },
        { "Ozds:Business:WithReactors", "false" },
        { "Ozds:Data:WithServices", "false" },
      });

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
      .AddOzdsTranslation(arguments);

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
