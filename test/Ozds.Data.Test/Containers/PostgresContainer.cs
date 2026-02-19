using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Ozds.Data.Test.Containers;

public sealed class PostgresContainer : IAsyncDisposable
{
  private const ushort PostgresPort = 5432;

  private const string PostgresDb = "ozds";

  private const string PostgresUser = "ozds";

  private const string PostgresPassword = "ozds";

  private const string PostgresReady = ".*listening on IPv4.*";

  private readonly IContainer container;

  private PostgresContainer(IContainer container)
  {
    this.container = container;
  }

  public string ConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append($"Server={container.Hostname}");
      builder.Append($";Port={container.GetMappedPublicPort(PostgresPort)}");
      builder.Append($";Database={PostgresDb}");
      builder.Append($";User Id={PostgresUser}");
      builder.Append($";Password={PostgresPassword}");
      return builder.ToString();
    }
  }

  public ValueTask DisposeAsync()
  {
    return container.DisposeAsync();
  }

  public static async Task<PostgresContainer> Create(
    CancellationToken cancellationToken
  )
  {
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    var wait = isWindows
      ? Wait.ForWindowsContainer().UntilMessageIsLogged(PostgresReady)
      : Wait.ForUnixContainer().UntilMessageIsLogged(PostgresReady);

    var container = new ContainerBuilder()
      .WithImage("timescale/timescaledb-ha:pg14-latest")
      .WithPortBinding(PostgresPort, true)
      .WithEnvironment("POSTGRES_DB", PostgresDb)
      .WithEnvironment("POSTGRES_USER", PostgresUser)
      .WithEnvironment("POSTGRES_PASSWORD", PostgresPassword)
      .WithWaitStrategy(wait)
      .Build();

    await container.StartAsync(cancellationToken);

    return new PostgresContainer(container);
  }
}
