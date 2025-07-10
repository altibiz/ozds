using System.Runtime.InteropServices;
using System.Text;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Ozds.Server.Test.Extensions;
using Assembly = System.Reflection.Assembly;

// TODO: actually use SSL to figure out for production

namespace Ozds.Server.Test.Containers;

public sealed class AutheliaContainer : IComposableService<AutheliaContainer>
{
  private const ushort AutheliaPort = 9091;

  private const string AutheliaClientId = "ozds";

  private const string AutheliaClientSecret = "ozds";

  private const string AutheliaCookieProtocol = "http";

  private const string AutheliaCookieDomain = "127.0.0.1";

  private const string AutheliaReady =
    "Listening for non-TLS connections";

  private const string AutheliaLogoutSubpath = "logout";

  private const string AutheliaIdKey = "preferred_username";

  private const string AutheliaIdClaim = "lldap_id";

  private readonly string certificatePem;

  private readonly string configFilePath;

  private readonly IContainer container;

#pragma warning disable S4487 // Unread "private" fields should be removed
  private readonly string host;
#pragma warning restore S4487 // Unread "private" fields should be removed

  private readonly int hostPort;

  private readonly string privateKeyPem;

  private readonly string tmpDir;

  private AutheliaContainer(
    IContainer container,
    string host,
    int hostPort,
    string tmpDir,
    string configFilePath,
    string privateKeyPem,
    string certificatePem
  )
  {
    this.container = container;
    this.host = host;
    this.hostPort = hostPort;
    this.tmpDir = tmpDir;
    this.configFilePath = configFilePath;
    this.privateKeyPem = privateKeyPem;
    this.certificatePem = certificatePem;
  }

  public string LogoutSubpath
  {
    get { return AutheliaLogoutSubpath; }
  }

  public string CookieProtocol
  {
    get { return AutheliaCookieProtocol; }
  }

  public string CookieDomain
  {
    get { return AutheliaCookieDomain; }
  }

  public string UserIdKey
  {
    get { return AutheliaIdKey; }
  }

  public string UserIdClaim
  {
    get { return AutheliaIdClaim; }
  }

  public string HostConnectionString
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append($"Authority={CookieProtocol}://{CookieDomain}:{hostPort}");
      builder.Append($";Client Id={AutheliaClientId}");
      builder.Append($";Client Secret={AutheliaClientSecret}");
      return builder.ToString();
    }
  }

  public string HttpBaseUrl
  {
    get
    {
      var builder = new StringBuilder();
      builder.Append("http://localhost");
      builder.Append($":{hostPort}");
      return builder.ToString();
    }
  }

  public static async Task<AutheliaContainer> Create(
    ContainerNetwork network,
    CancellationToken cancellationToken
  )
  {
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    var wait = isWindows
      ? Wait
        .ForWindowsContainer()
        .UntilMessageIsLogged(AutheliaReady)
      : Wait
        .ForUnixContainer()
        .UntilMessageIsLogged(AutheliaReady);

    var tmpDir = Path.Combine(
      Path.GetTempPath(),
      $"ozds-client-test-authelia-{Guid.NewGuid()}"
    );
    Directory.CreateDirectory(tmpDir);
    var configFilePath = Path.Combine(
      tmpDir,
      "configuration.yml"
    );

    var (privateKeyPem, certificatePem) = ServiceCryptography.Rs256KeyPair(
      "ozds-jwks-key-id"
    );

    var host = network.Host<AutheliaContainer>();
    var hostPort = network.Port<AutheliaContainer>();

    var container = new ContainerBuilder()
      .WithImage("authelia/authelia:4.39.4")
      .WithNetwork(network.Name)
      .WithHostname(host)
      .WithPortBinding(hostPort, AutheliaPort)
      .WithWaitStrategy(wait)
      .WithBindMount(
        configFilePath,
        "/config/configuration.yml",
        AccessMode.ReadWrite)
      .Build();
    await File.WriteAllTextAsync(configFilePath, "", cancellationToken);

    return new AutheliaContainer(
      container,
      host,
      hostPort,
      tmpDir,
      configFilePath,
      privateKeyPem,
      certificatePem
    );
  }

  public async Task Configure(
    ServiceComposition composition,
    CancellationToken cancellationToken
  )
  {
    var lldap = composition.Lldap;
    var ozds = composition.Ozds;
    var clientSecret =
      ServiceCryptography.GlibcPbkdf2Hash(AutheliaClientSecret);
    var escapedPrivateKeyPem = privateKeyPem.Escape();
    var escapedCertificatePem = certificatePem.Escape();
    var signOutCallbackSubpath = composition.Ozds.SignOutCallbackSubpath;
    var signInCallbackSubpath = composition.Ozds.SignInCallbackSubpath;

    var assembly = Assembly.GetExecutingAssembly();
    using var stream = assembly.GetManifestResourceStream(
        "Ozds.Server.Test.Assets.authelia-config.yaml.template")
      ?? throw new InvalidOperationException(
        "Lldap config template not found");
    using var reader = new StreamReader(stream);
    var configTemplate = await reader.ReadToEndAsync(cancellationToken);
    var configContent = configTemplate
      .Replace("{{AUTHELIA_LLDAP_HOST}}", lldap.Host)
      .Replace("{{AUTHELIA_LLDAP_PORT}}", lldap.Port.ToString())
      .Replace("{{AUTHELIA_LLDAP_BASE_DN}}", lldap.BaseDn)
      .Replace("{{AUTHELIA_LLDAP_USER_DN}}", lldap.UserDn)
      .Replace("{{AUTHELIA_LLDAP_PASSWORD}}", lldap.Password)
      .Replace("{{AUTHELIA_LLDAP_USERS_DN}}", lldap.UsersDn)
      .Replace("{{AUTHELIA_COOKIE_PROTOCOL}}", AutheliaCookieProtocol)
      .Replace("{{AUTHELIA_COOKIE_DOMAIN}}", AutheliaCookieDomain)
      .Replace("{{AUTHELIA_PORT}}", hostPort.ToString())
      .Replace("{{AUTHELIA_OZDS_DOMAIN}}", ozds.Domain)
      .Replace("{{AUTHELIA_OZDS_HTTP_PORT}}", ozds.HttpPort.ToString())
      .Replace("{{AUTHELIA_OZDS_HTTPS_PORT}}", ozds.HttpsPort.ToString())
      .Replace("{{AUTHELIA_JWKS_KEY_ID}}", "ozds-jwks-key-id")
      .Replace("{{AUTHELIA_JWKS_KEY}}", escapedPrivateKeyPem)
      .Replace("{{AUTHELIA_JWKS_CERTIFICATE_CHAIN}}", escapedCertificatePem)
      .Replace("{{AUTHELIA_OZDS_CLIENT_ID}}", AutheliaClientId)
      .Replace("{{AUTHELIA_OZDS_CLIENT_NAME}}", "OZDS")
      .Replace("{{AUTHELIA_OZDS_CLIENT_SECRET}}", clientSecret)
      .Replace("{{AUTHELIA_OZDS_SIGNIN_SUBPATH}}", signInCallbackSubpath)
      .Replace("{{AUTHELIA_OZDS_SIGNOUT_SUBPATH}}", signOutCallbackSubpath);
    await File.WriteAllTextAsync(
      configFilePath,
      configContent,
      cancellationToken
    );
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    await container.StartAsync(cancellationToken);
    Console.WriteLine($"Authelia started on '{HttpBaseUrl}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    Console.WriteLine($"Stopping Authelia on '{HttpBaseUrl}'...");
    await container.StopAsync(cancellationToken);
  }

  public async ValueTask DisposeAsync()
  {
    await container.DisposeAsync();
    Directory.Delete(tmpDir, true);
  }
}
