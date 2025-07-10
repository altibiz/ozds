using Ozds.Fake.Arguments;
using Ozds.Fake.Hosting;

namespace Ozds.Server.Test.Containers;

public sealed class AltibizFake : IComposableService<AltibizFake>
{
  private const string AltibizFakeNetworkUserInvoiceStateQueue =
    "altibiz-network-user-invoice-state";

  private OzdsFakeHost? host;

  private AltibizFake()
  {
  }

  public string NetworkUserInvoiceStateQueue
  {
    get { return AltibizFakeNetworkUserInvoiceStateQueue; }
  }

  public static Task<AltibizFake> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    return Task.FromResult(new AltibizFake());
  }

  public Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    var testBinDir = Directory.GetCurrentDirectory();
    var fakeDir = Path.GetFullPath(
      Path.Combine(
        testBinDir, "..", "..", "..", "..", "..", "scripts", "Ozds.Fake"));

    var arguments = new OzdsFakeAltibizArguments();

    var settings = new HostApplicationBuilderSettings
    {
      EnvironmentName = "Development",
      ApplicationName = "Ozds.Fake",
      ContentRootPath = fakeDir
    };

    host = new OzdsFakeHost(
      arguments, settings, builder =>
      {
        builder.Configuration.AddInMemoryCollection(
          new Dictionary<string, string?>
          {
            {
              "Ozds:Fake:Messaging:ConnectionString",
              composition.RabbitMq.HostConnectionString
            },
            {
              "Ozds:Fake:Messaging:Endpoints:InitiateNetworkUserInvoice",
              $"queue:{composition.Ozds.NetworkUserInvoiceStateQueue}"
            },
            {
              "Ozds:Fake:Messaging:Endpoints:AbortNetworkUserInvoice",
              $"queue:{composition.Ozds.NetworkUserInvoiceStateQueue}"
            },
            {
              "Ozds:Fake:Messaging:Endpoints:RegisterNetworkUserInvoice",
              $"queue:{composition.Ozds.NetworkUserInvoiceStateQueue}"
            },
            {
              "Ozds:Fake:Messaging:Endpoints:ApproveNetworkUserInvoice",
              $"queue:{composition.Ozds.NetworkUserInvoiceStateQueue}"
            },
            {
              "Ozds:Fake:Messaging:Sagas:NetworkUserInvoiceState",
              NetworkUserInvoiceStateQueue
            }
          });
      });

    return Task.CompletedTask;
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    if (host is null)
    {
      throw new InvalidOperationException(
        "Altibiz fake not configured"
      );
    }

    await host.StartAsync(cancellationToken);
    Console.WriteLine("Altibiz fake started.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    if (host is null)
    {
      throw new InvalidOperationException(
        "Altibiz fake not configured"
      );
    }

    Console.WriteLine("Stopping Altibiz fake...");
    await host.StopAsync(cancellationToken);
  }

  public ValueTask DisposeAsync()
  {
    if (host is not null)
    {
      host.Dispose();
    }

    return ValueTask.CompletedTask;
  }
}
