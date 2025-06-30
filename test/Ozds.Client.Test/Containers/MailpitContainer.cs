using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace Ozds.Client.Test.Containers;

public sealed class MailpitContainer : IComposableService<MailpitContainer>
{
  private const ushort MailpitSmtpPort = 1025;

  private const ushort MailpitHttpPort = 8025;

  private const string MailpitUser = "ozds";

  private const string MailpitPassword = "ozds";

  private const string MailpitReady =
    """.*\[http\] accessible via.*""";

  private readonly string authFilePath;

  private readonly IContainer container;

#pragma warning disable S4487 // Unread "private" fields should be removed
  private readonly string host;
#pragma warning restore S4487 // Unread "private" fields should be removed

  private readonly int hostHttpPort;

  private readonly int hostSmtpPort;

  private readonly string tmpDir;

  private MailpitContainer(
    IContainer container,
    string host,
    int hostSmtpPort,
    int hostHttpPort,
    string tmpDir,
    string authFilePath
  )
  {
    this.container = container;
    this.host = host;
    this.hostSmtpPort = hostSmtpPort;
    this.hostHttpPort = hostHttpPort;
    this.tmpDir = tmpDir;
    this.authFilePath = authFilePath;
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

  public static async Task<MailpitContainer> Create(
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

    var tmpDir = Path.Combine(
      Path.GetTempPath(),
      $"ozds-client-test-mailpit-{Guid.NewGuid()}"
    );
    Directory.CreateDirectory(tmpDir);
    var authFilePath = Path.Combine(tmpDir, "mailpit.auth");
    await File.WriteAllTextAsync(authFilePath, string.Empty, cancellationToken);

    var host = network.Host<MailpitContainer>();
    var hostSmtpPort = network.Port<MailpitContainer>("smtp");
    var hostHttpPort = network.Port<MailpitContainer>("http");
    var container = new ContainerBuilder()
      .WithImage("axllent/mailpit:v1.26")
      .WithNetwork(network.Name)
      .WithHostname(host)
      .WithPortBinding(hostSmtpPort, MailpitSmtpPort)
      .WithPortBinding(hostHttpPort, MailpitHttpPort)
      .WithEnvironment("MP_UI_AUTH_FILE", "/etc/mailpit/mailpit.auth")
      .WithBindMount(
        authFilePath,
        "/etc/mailpit/mailpit.auth",
        AccessMode.ReadWrite
      )
      .WithWaitStrategy(wait)
      .Build();

    return new MailpitContainer(
      container, host, hostSmtpPort, hostHttpPort, tmpDir, authFilePath);
  }

  public async Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    var bcryptHash = ServiceCryptography.BcryptHash(MailpitPassword);
    await File.WriteAllTextAsync(
      authFilePath,
      $"{MailpitUser}:{bcryptHash}",
      cancellationToken
    );
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
    Directory.Delete(tmpDir, true);
  }
}
