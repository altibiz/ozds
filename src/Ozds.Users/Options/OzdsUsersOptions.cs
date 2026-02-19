using Microsoft.Extensions.Options;

namespace Ozds.Users.Options;

public class OzdsUsersOptions
{
  public OzdsUsersOptionsOidc Oidc { get; set; } = new();

  public OzdsUsersOptionsLdap Ldap { get; set; } = new();

  public bool WithAuth { get; set; } = true;
}

public class OzdsUsersOptionsOidc
{
  public string ConnectionString { get; set; } = string.Empty;

  public bool? RequireHttpsMetadata { get; set; } = true;

  public string? AuthLogoutSubpath { get; set; } = default;

  public string UserIdKey { get; set; } = string.Empty;

  public string UserIdClaim { get; set; } = string.Empty;

  public string SignInCallbackSubpath { get; set; } = "signin-oidc";

  public string SignOutCallbackSubpath { get; set; } = "signout-oidc";
}

public class OzdsUsersOptionsLdap
{
  public string ConnectionString { get; set; } = string.Empty;

  public List<string> UserObjectClasses { get; set; } = new();

  public string UserFilterObjectClass { get; set; } = string.Empty;

  public string UserOrganizationalUnit { get; set; } = string.Empty;

  public string BaseDn { get; set; } = string.Empty;

  public string UserIdAttribute { get; set; } = string.Empty;

  public string UserNameAttribute { get; set; } = string.Empty;

  public string UserEmailAttribute { get; set; } = string.Empty;
}

public class OzdsUsersParsedOidcConnectionString
{
  public OzdsUsersParsedOidcConnectionString(string connectionString)
  {
    var dictionary = connectionString
      .Split(';')
      .ToDictionary(
        x => x.Split('=')[0],
        x => string.Join('=', x.Split('=')[1..])
      );

    Authority = dictionary["Authority"];
    ClientId = dictionary["Client Id"];
    ClientSecret = dictionary["Client Secret"];
  }

  public string Authority { get; set; }

  public string ClientId { get; set; }

  public string ClientSecret { get; set; }
}

public class OzdsUsersParsedLdapConnectionString
{
  public OzdsUsersParsedLdapConnectionString(string connectionString)
  {
    var dictionary = connectionString
      .Split(';')
      .ToDictionary(
        x => x.Split('=')[0],
        x => string.Join('=', x.Split('=')[1..])
      );

    Host = dictionary["Host"];
    Port = int.Parse(dictionary["Port"]);
    User = dictionary["UserDn"];
    Password = dictionary["Password"];
    Ssl = bool.Parse(dictionary["Ssl"]);
  }

  public string Host { get; set; }

  public int Port { get; set; }

  public string User { get; set; }

  public string Password { get; set; }

  public bool Ssl { get; set; }
}

public class ConfigureOzdsUsersOptions(IConfiguration configuration)
  : IConfigureOptions<OzdsUsersOptions>
{
  public void Configure(OzdsUsersOptions options)
  {
    configuration.GetSection("Ozds:Users").Bind(options);
  }

  public static OzdsUsersParsedOidcConnectionString OidcConnectionString(
    IConfiguration configuration
  )
  {
    var connectionString =
      configuration.GetValue<string?>("Ozds:Users:Oidc:ConnectionString")
      ?? string.Empty;

    return new OzdsUsersParsedOidcConnectionString(connectionString);
  }

  public static OzdsUsersParsedLdapConnectionString LdapConnectionString(
    IConfiguration configuration
  )
  {
    var connectionString =
      configuration.GetValue<string?>("Ozds:Users:Ldap:ConnectionString")
      ?? string.Empty;

    return new OzdsUsersParsedLdapConnectionString(connectionString);
  }

  public static bool? RequireHttpsMetadata(IConfiguration configuration)
  {
    return configuration.GetValue<bool?>(
      "Ozds:Users:Oidc:RequireHttpsMetadata"
    );
  }

  public static bool WithAuth(IConfiguration configuration)
  {
    return configuration.GetValue<bool?>("Ozds:Users:WithAuth") ?? true;
  }

  public static string? AuthLogoutSubpath(IConfiguration configuration)
  {
    return configuration.GetValue<string?>("Ozds:Users:Oidc:AuthLogoutSubpath")
      ?? default;
  }

  public static string IdKey(IConfiguration configuration)
  {
    return configuration.GetValue<string?>("Ozds:Users:Oidc:UserIdKey")
      ?? string.Empty;
  }

  public static string IdClaim(IConfiguration configuration)
  {
    return configuration.GetValue<string?>("Ozds:Users:Oidc:UserIdClaim")
      ?? string.Empty;
  }

  public static string SignInCallbackSubpath(IConfiguration configuration)
  {
    return configuration.GetValue<string?>(
        "Ozds:Users:Oidc:SignInCallbackSubpath"
      ) ?? string.Empty;
  }

  public static string SignOutCallbackSubpath(IConfiguration configuration)
  {
    return configuration.GetValue<string?>(
        "Ozds:Users:Oidc:SignOutCallbackSubpath"
      ) ?? string.Empty;
  }
}
