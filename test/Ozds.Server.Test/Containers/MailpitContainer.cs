using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace Ozds.Server.Test.Containers;

public sealed class MailpitContainer : IComposableService<MailpitContainer>
{
  private const ushort MailpitSmtpPort = 1025;

  private const ushort MailpitHttpPort = 8025;

  private const string MailpitUser = "ozds";

  private const string MailpitPassword = "ozds";

  private const string MailpitReady =
    """.*\[http\] accessible via.*""";

  private readonly IContainer container;

#pragma warning disable S4487 // Unread "private" fields should be removed
  private readonly string host;
#pragma warning restore S4487 // Unread "private" fields should be removed

  private readonly int hostHttpPort;

  private readonly int hostSmtpPort;

  private MailpitContainer(
    IContainer container,
    string host,
    int hostSmtpPort,
    int hostHttpPort
  )
  {
    this.container = container;
    this.host = host;
    this.hostSmtpPort = hostSmtpPort;
    this.hostHttpPort = hostHttpPort;
  }

  public string NetworkConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append($"Host={host}");
      builder.Append($";Port={MailpitSmtpPort}");
      builder.Append($";User={MailpitUser}");
      builder.Append($";Password={MailpitPassword}");
      builder.Append(";Ssl=false");
      return builder.ToString();
    }
  }

  public string HostConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append("Host=localhost");
      builder.Append($";Port={hostSmtpPort}");
      builder.Append($";User={MailpitUser}");
      builder.Append($";Password={MailpitPassword}");
      builder.Append(";Ssl=false");
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

  public static Task<MailpitContainer> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    var wait = isWindows
      ? Wait
        .ForWindowsContainer()
        .UntilMessageIsLogged(MailpitReady)
      : Wait
        .ForUnixContainer()
        .UntilMessageIsLogged(MailpitReady);

    var host = network.Host<MailpitContainer>();
    var hostSmtpPort = network.Port<MailpitContainer>("smtp");
    var hostHttpPort = network.Port<MailpitContainer>("http");
    var container = new ContainerBuilder()
      .WithImage("axllent/mailpit:v1.26")
      .WithNetwork(network.Name)
      .WithHostname(host)
      .WithPortBinding(hostSmtpPort, MailpitSmtpPort)
      .WithPortBinding(hostHttpPort, MailpitHttpPort)
      .WithEnvironment("MP_UI_AUTH", $"{MailpitUser}:{MailpitPassword}")
      .WithEnvironment("MP_SEND_API_AUTH", $"{MailpitUser}:{MailpitPassword}")
      .WithEnvironment("MP_SMTP_AUTH", $"{MailpitUser}:{MailpitPassword}")
      .WithEnvironment("MP_SMTP_AUTH_ALLOW_INSECURE", "true")
      .WithWaitStrategy(wait)
      .Build();

    return Task.FromResult(
      new MailpitContainer(
        container, host, hostSmtpPort, hostHttpPort));
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
    Console.WriteLine($"Mailpit started on '{HttpBaseUrl}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    Console.WriteLine($"Stopping Mailpit on '{HttpBaseUrl}'...");
    await container.StopAsync(cancellationToken);
  }

  public async ValueTask DisposeAsync()
  {
    await container.DisposeAsync();
  }
}
