using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Ozds.Server.Test.Containers;

public sealed class RabbitMqContainer : IComposableService<RabbitMqContainer>
{
  private const ushort RabbitMqAmqpPort = 5672;

  private const ushort RabbitMqHttpPort = 15672;

  private const string RabbitMqVirtualHost = "/";

  private const string RabbitMqUser = "ozds";

  private const string RabbitMqPassword = "ozds";

  private const string RabbitMqReady = ".*Time to start RabbitMQ.*";

  private readonly IContainer container;

  private readonly string host;

  private readonly int hostAmqpPort;

  private readonly int hostHttpPort;

  private RabbitMqContainer(
    IContainer container,
    string host,
    int hostAmqpPort,
    int hostHttpPort
  )
  {
    this.container = container;
    this.host = host;
    this.hostAmqpPort = hostAmqpPort;
    this.hostHttpPort = hostHttpPort;
  }

  public string NetworkConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append($"amqp://Host={host}");
      builder.Append($";Port={RabbitMqAmqpPort}");
      builder.Append($";VirtualHost={RabbitMqVirtualHost}");
      builder.Append($";Username={RabbitMqUser}");
      builder.Append($";Password={RabbitMqPassword}");
      return builder.ToString();
    }
  }

  public string HostConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append("amqp://Host=localhost");
      builder.Append($";Port={hostAmqpPort}");
      builder.Append($";VirtualHost={RabbitMqVirtualHost}");
      builder.Append($";Username={RabbitMqUser}");
      builder.Append($";Password={RabbitMqPassword}");
      return builder.ToString();
    }
  }

  public string HttpBaseUrl
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append("http://localhost");
      builder.Append($":{hostHttpPort}");
      return builder.ToString();
    }
  }

  public static Task<RabbitMqContainer> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var wait = Wait.ForUnixContainer().UntilMessageIsLogged(RabbitMqReady);

    var host = network.Host<RabbitMqContainer>();
    var hostAmqpPort = network.Port<RabbitMqContainer>();
    var hostHttpPort = network.Port<RabbitMqContainer>("http");
    var container = new ContainerBuilder("rabbitmq:3.13-management")
      .WithNetwork(network.Name)
      .WithHostname(host)
      .WithPortBinding(hostAmqpPort, RabbitMqAmqpPort)
      .WithPortBinding(hostHttpPort, RabbitMqHttpPort)
      .WithEnvironment("RABBITMQ_DEFAULT_USER", RabbitMqUser)
      .WithEnvironment("RABBITMQ_DEFAULT_PASS", RabbitMqPassword)
      .WithWaitStrategy(wait)
      .Build();

    return Task.FromResult(
      new RabbitMqContainer(container, host, hostAmqpPort, hostHttpPort)
    );
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
    Console.WriteLine($"RabbitMQ started on '{HttpBaseUrl}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    Console.WriteLine($"Stopping RabbitMQ on '{HttpBaseUrl}'...");
    await container.StopAsync(cancellationToken);
  }

  public async ValueTask DisposeAsync()
  {
    await container.DisposeAsync();
  }
}
