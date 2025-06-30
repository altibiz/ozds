using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Ozds.Client.Test.Containers;

public sealed class PostgresContainer : IComposableService<PostgresContainer>
{
  private const ushort PostgresPort = 5432;

  private const string PostgresDb = "ozds";

  private const string PostgresUser = "ozds";

  private const string PostgresPassword = "ozds";

  private const string PostgresReady =
    "database system is ready to accept connections";

  private readonly IContainer container;

  private readonly string host;

  private readonly int hostPort;

  private PostgresContainer(
    IContainer container,
    string host,
    int hostPort
  )
  {
    this.container = container;
    this.host = host;
    this.hostPort = hostPort;
  }

  public string NetworkConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append($"Server={host}");
      builder.Append($";Port={PostgresPort}");
      builder.Append($";Database={PostgresDb}");
      builder.Append($";User Id={PostgresUser}");
      builder.Append($";Password={PostgresPassword}");
      return builder.ToString();
    }
  }

  public string HostConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append("Server=localhost");
      builder.Append($";Port={hostPort}");
      builder.Append($";Database={PostgresDb}");
      builder.Append($";User Id={PostgresUser}");
      builder.Append($";Password={PostgresPassword}");
      return builder.ToString();
    }
  }

  public string PsqlConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append("postgres://");
      builder.Append($"{PostgresUser}:{PostgresPassword}");
      builder.Append($"@localhost:{hostPort}");
      builder.Append($"/{PostgresDb}");
      builder.Append("?sslmode=disable");
      return builder.ToString();
    }
  }

  public static Task<PostgresContainer> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    var wait = isWindows
      ? Wait
        .ForWindowsContainer()
        .UntilMessageIsLogged(PostgresReady)
      : Wait
        .ForUnixContainer()
        .UntilMessageIsLogged(PostgresReady);

    var host = network.Host<PostgresContainer>();
    var hostPort = network.Port<PostgresContainer>();
    var container = new ContainerBuilder()
      .WithImage("timescale/timescaledb-ha:pg14-latest")
      .WithNetwork(network.Name)
      .WithHostname(host)
      .WithPortBinding(hostPort, PostgresPort)
      .WithEnvironment("POSTGRES_DB", PostgresDb)
      .WithEnvironment("POSTGRES_USER", PostgresUser)
      .WithEnvironment("POSTGRES_PASSWORD", PostgresPassword)
      .WithWaitStrategy(wait)
      .Build();

    return Task.FromResult(new PostgresContainer(container, host, hostPort));
  }

  public Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    return Task.CompletedTask;
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    await container.StartAsync(cancellationToken);
    Console.WriteLine($"Postgres started on '{PsqlConnectionString}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    Console.WriteLine($"Stopping Postgres on '{PsqlConnectionString}'...");
    await container.StopAsync(cancellationToken);
  }

  public async ValueTask DisposeAsync()
  {
    await container.DisposeAsync();
  }
}
