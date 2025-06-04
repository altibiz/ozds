using Moq;
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
using MessagingMessageSender =
  Ozds.Messaging.Sender.Abstractions.IMessageSender;

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
            { "Ozds:Messaging:WithBus", "false" }
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
      .AddOzdsIot();

    // NOTE: hacks to enable most Ozds services working
    builder.Services.AddSingleton(Mock.Of<MessagingMessageSender>());
    foreach (var service in builder.Services
      .Where(
        service =>
          service.ServiceType == typeof(IHostedService)
          && !(service.ImplementationInstance?.GetType().Namespace
            ?.StartsWith(nameof(Microsoft)) ?? false)
          && !(service.ImplementationType?.Namespace
            ?.StartsWith(nameof(Microsoft)) ?? false)
          && !(service.ImplementationFactory?.Method?.Module.Name
            ?.StartsWith(nameof(Microsoft)) ?? false))
      .ToList())
    {
      builder.Services.Remove(service);
    }

    builder.AddOzdsMigration(arguments);

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
