using Ozds.Fake.Arguments;
using Ozds.Fake.Extensions;
using Ozds.Sdk.Extensions;
using Ozds.Server.Hosting;

namespace Ozds.Server.Test.Containers;

public sealed class OzdsServer : IComposableService<OzdsServer>
{

  private const string OzdsNetworkUserInvoiceStateQueue =
    "ozds-network-user-invoice-state";

  private const string OzdsEmailFromName = "AltiBiz";

  private const string OzdsEmailFromAddress = "noreply@altibiz.com";

  private const string OzdsDomain = "localhost";

  private Action<IHostApplicationBuilder>? configureHost;

  private OzdsServerHost? host;

  private OzdsServer(int httpPort, int httpsPort)
  {
    HttpPort = httpPort;
    HttpsPort = httpsPort;
  }

  public string Domain
  {
    get { return OzdsDomain; }
  }

  public int HttpsPort { get; }

  public int HttpPort { get; }

  public string NetworkUserInvoiceStateQueue
  {
    get { return OzdsNetworkUserInvoiceStateQueue; }
  }

  public string EmailFromName
  {
    get { return OzdsEmailFromName; }
  }

  public string EmailFromAddress
  {
    get { return OzdsEmailFromAddress; }
  }

  public string HttpsBaseUrl
  {
    get { return $"https://{OzdsDomain}:{HttpsPort}"; }
  }

  public string HttpBaseUrl
  {
    get { return $"http://{OzdsDomain}:{HttpPort}"; }
  }

  public IServiceProvider Services
  {
    get
    {
      if (host is null)
      {
        throw new InvalidOperationException("Ozds server not configured");
      }

      return host.Services;
    }
  }

  public static Task<OzdsServer> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var httpPort = network.Port<OzdsServer>("http");
    var httpsPort = network.Port<OzdsServer>("https");

    return Task.FromResult(new OzdsServer(httpPort, httpsPort));
  }

  public Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    var urls = $"{HttpsBaseUrl};{HttpBaseUrl}";

    var testBinDir = Directory.GetCurrentDirectory();
    var serverDir = Path.GetFullPath(
      Path.Combine(
        testBinDir,
        "..",
        "..",
        "..",
        "..",
        "..",
        "src",
        "Ozds.Server"
      )
    );

    var args = new[]
    {
      "--urls",
      urls,
      "--contentroot",
      serverDir,
      "--environment",
      "Development",
      "--applicationname",
      "Ozds.Server",
    };

    host = new OzdsServerHost(
      args,
      appBuilder =>
      {
        var dictionary = new Dictionary<string, string?>
        {
          {
            "Ozds:Data:ConnectionString",
            composition.Postgres.HostConnectionString
          },
          { "Ozds:Data:MigrateOnStartup", "true" },
          {
            "Ozds:Messaging:ConnectionString",
            composition.RabbitMq.HostConnectionString
          },
          {
            "Ozds:Messaging:PersistenceConnectionString",
            composition.Postgres.HostConnectionString
          },
          { "Ozds:Messaging:MigrateOnStartup", "true" },
          {
            "Ozds:Messaging:Endpoints:AcknowledgeNetworkUserInvoice",
            $"queue:{composition.Altibiz.NetworkUserInvoiceStateQueue}"
          },
          {
            "Ozds:Messaging:Sagas:NetworkUserInvoiceState",
            NetworkUserInvoiceStateQueue
          },
          {
            "Ozds:Email:Smtp:ConnectionString",
            composition.Mailpit.HostConnectionString
          },
          { "Ozds:Email:From:Name", EmailFromName },
          { "Ozds:Email:From:Address", EmailFromAddress },
          {
            "Ozds:Jobs:ConnectionString",
            composition.Postgres.HostConnectionString
          },
          { "Ozds:Jobs:MigrateOnStartup", "true" },
          {
            "Ozds:Users:ConnectionString",
            composition.Postgres.HostConnectionString
          },
          { "Ozds:Users:MigrateOnStartup", "true" },
          { "Ozds:Fake:Client:BaseUrl", HttpBaseUrl },
          { "Ozds:Sdk:BaseUrl", HttpBaseUrl },
        };

        appBuilder.Configuration.AddInMemoryCollection(dictionary);

        if (configureHost is not null)
        {
          configureHost(appBuilder);
        }

        appBuilder.AddOzdsFake(new OzdsFakeBypassArguments());
        appBuilder.AddOzdsSdk();
      }
    );

    return Task.CompletedTask;
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    // NOTE: this makes it start...
    if (host is null)
    {
      throw new InvalidOperationException("Ozds server not configured");
    }

    await host.StartAsync(cancellationToken);
    Console.WriteLine($"Ozds started on '{HttpBaseUrl}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    if (host is null)
    {
      throw new InvalidOperationException("Ozds server not configured");
    }

    Console.WriteLine($"Stopping Ozds on '{HttpBaseUrl}'...");
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

  public void ConfigureHost(
    Action<IHostApplicationBuilder>? configureHost = null
  )
  {
    this.configureHost = configureHost;
  }
}
