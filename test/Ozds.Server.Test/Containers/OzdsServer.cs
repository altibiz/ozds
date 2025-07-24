using Ozds.Fake.Arguments;
using Ozds.Fake.Extensions;
using Ozds.Server.Hosting;

namespace Ozds.Server.Test.Containers;

public sealed class OzdsServer : IComposableService<OzdsServer>
{
  private const string OzdsSignInCallbackSubpath = "signin-oidc";

  private const string OzdsSignOutCallbackSubpath = "signout-oidc";

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

  public string SignInCallbackSubpath
  {
    get { return OzdsSignInCallbackSubpath; }
  }

  public string SignOutCallbackSubpath
  {
    get { return OzdsSignOutCallbackSubpath; }
  }

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
        throw new InvalidOperationException(
          "Ozds server not configured"
        );
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

    if ((Domain == "127.0.0.1" || Domain == "localhost")
      && composition.Authelia.CookieDomain != "127.0.0.1"
      && composition.Authelia.CookieDomain != "localhost")
    {
      var autheliaHttpsUrl =
        $"https://{composition.Authelia.CookieDomain}:{HttpsPort}";
      var autheliaHttpUrl =
        $"http://{composition.Authelia.CookieDomain}:{HttpPort}";

      urls = $"{autheliaHttpsUrl};{autheliaHttpUrl};{urls}";
    }

    var testBinDir = Directory.GetCurrentDirectory();
    var serverDir = Path.GetFullPath(
      Path.Combine(
        testBinDir, "..", "..", "..", "..", "..", "src", "Ozds.Server"));

    var args = new[]
    {
      "--urls",
      urls,
      "--contentroot",
      serverDir,
      "--environment",
      "Development",
      "--applicationname",
      "Ozds.Server"
    };

    host = new OzdsServerHost(
      args, appBuilder =>
      {
        var dictionary = new Dictionary<string, string?>
        {
          {
            "Ozds:Data:ConnectionString",
            composition.Postgres.HostConnectionString
          },
          {
            "Ozds:Data:MigrateOnStartup",
            "true"
          },
          {
            "Ozds:Messaging:ConnectionString",
            composition.RabbitMq.HostConnectionString
          },
          {
            "Ozds:Messaging:PersistenceConnectionString",
            composition.Postgres.HostConnectionString
          },
          {
            "Ozds:Messaging:MigrateOnStartup",
            "true"
          },
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
          {
            "Ozds:Email:From:Name",
            EmailFromName
          },
          {
            "Ozds:Email:From:Address",
            EmailFromAddress
          },
          {
            "Ozds:Jobs:ConnectionString",
            composition.Postgres.HostConnectionString
          },
          {
            "Ozds:Jobs:MigrateOnStartup",
            "true"
          },
          {
            "Ozds:Users:Oidc:ConnectionString",
            composition.Authelia.HostConnectionString
          },
          {
            "Ozds:Users:Oidc:AuthLogoutSubpath",
            composition.Authelia.LogoutSubpath
          },
          {
            "Ozds:Users:Oidc:UserIdKey",
            composition.Authelia.UserIdKey
          },
          {
            "Ozds:Users:Oidc:UserIdClaim",
            composition.Authelia.UserIdClaim
          },
          {
            "Ozds:Users:Oidc:SignInCallbackSubpath",
            SignInCallbackSubpath
          },
          {
            "Ozds:Users:Oidc:SignOutCallbackSubpath",
            SignOutCallbackSubpath
          },
          {
            "Ozds:Users:Ldap:ConnectionString",
            composition.Lldap.HostConnectionString
          },
          {
            "Ozds:Users:Ldap:UserFilterObjectClass",
            composition.Lldap.UserFilterObjectClass
          },
          {
            "Ozds:Users:Ldap:UserOrganizationalUnit",
            composition.Lldap.UserOrganizationalUnit
          },
          {
            "Ozds:Users:Ldap:BaseDn",
            composition.Lldap.BaseDn
          },
          {
            "Ozds:Users:Ldap:UserIdAttribute",
            composition.Lldap.UserIdAttribute
          },
          {
            "Ozds:Users:Ldap:UserNameAttribute",
            composition.Lldap.UserNameAttribute
          },
          {
            "Ozds:Users:Ldap:UserEmailAttribute",
            composition.Lldap.UserEmailAttribute
          },
          {
            "Ozds:Fake:Client:BaseUrl",
            HttpBaseUrl
          }
        };

        foreach (var (userObjectClass, index) in
          composition.Lldap.UserObjectClasses.Select((x, i) => (x, i)))
        {
          dictionary.Add(
            $"Ozds:Users:Ldap:UserObjectClasses:{index}",
            userObjectClass);
        }

        appBuilder.Configuration.AddInMemoryCollection(dictionary);

        if (configureHost is not null)
        {
          configureHost(appBuilder);
        }

        appBuilder.AddOzdsFake(new OzdsFakeBypassArguments());
      });

    return Task.CompletedTask;
  }

  public async Task Start(CancellationToken cancellationToken)
  {
    // NOTE: this makes it start...
    if (host is null)
    {
      throw new InvalidOperationException(
        "Ozds server not configured"
      );
    }

    await host.StartAsync(cancellationToken);
    Console.WriteLine($"Ozds started on '{HttpBaseUrl}'.");
  }

  public async Task Stop(CancellationToken cancellationToken)
  {
    if (host is null)
    {
      throw new InvalidOperationException(
        "Ozds server not configured"
      );
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
    Action<IHostApplicationBuilder>? configureHost = null)
  {
    this.configureHost = configureHost;
  }
}
