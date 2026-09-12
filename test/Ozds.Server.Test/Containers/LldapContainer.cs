using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Assembly = System.Reflection.Assembly;

namespace Ozds.Server.Test.Containers;

public sealed class LldapContainer : IComposableService<LldapContainer>
{
  private const ushort LldapLdapPort = 3890;

  private const ushort LldapHttpPort = 17170;

  private const string LldapBaseDn = "dc=altibiz,dc=com";

  private const string LldapUsersDn = "ou=people";

  private const string LldapUser = "admin";

  private const string LldapUserDn =
    $"cn={LldapUser},{LldapUsersDn},{LldapBaseDn}";

  private const string LldapPassword = "admin-ozds";

  private const string LldapReady = ".*DB Cleanup Cron started.*";

  private const string LldapUserFilterObjectClass = "person";

  private const string LldapUserOrganizationalUnit = "people";

  private const string LldapUserIdAttribute = "uid";

  private const string LldapUserNameAttribute = "cn";

  private const string LldapUserEmailAttribute = "mail";

  private static readonly string[] LldapUserObjectClasses =
  [
    "inetOrgPerson",
    "posixAccount",
    "shadowAccount",
    "top",
  ];

  private readonly string configFilePath;

  private readonly IContainer container;

  private readonly int hostHttpPort;

  private readonly string tmpDir;

  private LldapContainer(
    IContainer container,
    string host,
    int hostLdapPort,
    int hostHttpPort,
    string tmpDir,
    string configFilePath
  )
  {
    this.container = container;
    Host = host;
    HostPort = hostLdapPort;
    this.hostHttpPort = hostHttpPort;
    this.tmpDir = tmpDir;
    this.configFilePath = configFilePath;
  }

  public string Host { get; }

  public int Port
  {
    get { return LldapLdapPort; }
  }

  public int HostPort { get; }

  public string BaseDn
  {
    get { return LldapBaseDn; }
  }

  public string UsersDn
  {
    get { return LldapUsersDn; }
  }

  public string UserDn
  {
    get { return LldapUserDn; }
  }

  public string Password
  {
    get { return LldapPassword; }
  }

  public List<string> UserObjectClasses
  {
    get { return LldapUserObjectClasses.ToList(); }
  }

  public string UserFilterObjectClass
  {
    get { return LldapUserFilterObjectClass; }
  }

  public string UserOrganizationalUnit
  {
    get { return LldapUserOrganizationalUnit; }
  }

  public string UserIdAttribute
  {
    get { return LldapUserIdAttribute; }
  }

  public string UserNameAttribute
  {
    get { return LldapUserNameAttribute; }
  }

  public string UserEmailAttribute
  {
    get { return LldapUserEmailAttribute; }
  }

  public string NetworkConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append($"Host={Host}");
      builder.Append($";Port={LldapLdapPort}");
      builder.Append($";UserDn={LldapUserDn}");
      builder.Append($";Password={LldapPassword}");
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
      builder.Append($";Port={HostPort}");
      builder.Append($";UserDn={LldapUserDn}");
      builder.Append($";Password={LldapPassword}");
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

  public static async Task<LldapContainer> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var wait = Wait.ForUnixContainer().UntilMessageIsLogged(LldapReady);

    var tmpDir = Path.Combine(
      Path.GetTempPath(),
      $"ozds-client-test-lldap-{Guid.NewGuid()}"
    );
    Directory.CreateDirectory(tmpDir);
    var configFilePath = Path.Combine(tmpDir, "lldap_config.toml");
    await File.WriteAllTextAsync(configFilePath, "", cancellationToken);

    var host = network.Host<LldapContainer>();
    var hostLdapPort = network.Port<LldapContainer>("ldap");
    var hostHttpPort = network.Port<LldapContainer>("http");
    var container = new ContainerBuilder("lldap/lldap:2025-05-19")
      .WithNetwork(network.Name)
      .WithHostname(host)
      .WithPortBinding(hostLdapPort, LldapLdapPort)
      .WithPortBinding(hostHttpPort, LldapHttpPort)
      .WithWaitStrategy(wait)
      .WithBindMount(
        configFilePath,
        "/data/lldap_config.toml",
        AccessMode.ReadWrite
      )
      .Build();

    return new LldapContainer(
      container,
      host,
      hostLdapPort,
      hostHttpPort,
      tmpDir,
      configFilePath
    );
  }

  public async Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    var assembly = Assembly.GetExecutingAssembly();
    using var stream =
      assembly.GetManifestResourceStream(
        "Ozds.Server.Test.Assets.lldap-config.toml.template"
      )
      ?? throw new InvalidOperationException("Lldap config template not found");
    using var reader = new StreamReader(stream);
    var configTemplate = await reader.ReadToEndAsync(cancellationToken);
    var configContent = configTemplate
      .Replace("{{LLDAP_BASE_DN}}", LldapBaseDn)
      .Replace("{{LLDAP_USER}}", LldapUser)
      .Replace("{{LLDAP_PASSWORD}}", LldapPassword);
    await File.WriteAllTextAsync(
      configFilePath,
      configContent,
      cancellationToken
    );
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    await container.StartAsync(cancellationToken);
    Console.WriteLine($"Lldap started on '{HttpBaseUrl}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    Console.WriteLine($"Stopping Lldap on '{HttpBaseUrl}'...");
    await container.StopAsync(cancellationToken);
  }

  public async ValueTask DisposeAsync()
  {
    await container.DisposeAsync();
    Directory.Delete(tmpDir, true);
  }
}
