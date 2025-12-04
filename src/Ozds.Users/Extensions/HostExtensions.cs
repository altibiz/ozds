using Altibiz.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Novell.Directory.Ldap;
using Ozds.Users.Mutations.Abstractions;
using Ozds.Users.Options;
using Ozds.Users.Queries.Abstractions;

namespace Ozds.Users.Extensions;

public static class HostExtensions
{
  public const string AuthenticationScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;

  public const string ChallengeScheme =
    OpenIdConnectDefaults.AuthenticationScheme;

  public static IHostApplicationBuilder AddOzdsUsers(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddQueries();
    builder.AddMutations();
    builder.AddHttp();
    builder.AddLdap();
    if (ConfigureOzdsUsersOptions.WithAuth(builder.Configuration))
    {
      builder.AddOidc();
    }

    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsUsersOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddQueries(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IQueries));
    return builder;
  }

  private static IHostApplicationBuilder AddMutations(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScopedAssignableTo(typeof(IMutations));
    return builder;
  }

  private static IHostApplicationBuilder AddHttp(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddHttpContextAccessor();
    return builder;
  }

  private static IHostApplicationBuilder AddLdap(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddScoped(
      serviceProvider =>
      {
        var connectionString = ConfigureOzdsUsersOptions
          .LdapConnectionString(builder.Configuration);

        var options = new LdapConnectionOptions();
        if (builder.Environment.IsDevelopment())
        {
          options.ConfigureRemoteCertificateValidationCallback(
            (sender, certificate, chain, errors) => { return true; });
        }

        var connection = new LdapConnection(options);
        if (connectionString.Ssl)
        {
          connection.SecureSocketLayer = true;
        }

        connection.Connect(connectionString.Host, connectionString.Port);
        connection.Bind(connectionString.User, connectionString.Password);

        return connection;
      });

    return builder;
  }

  private static IHostApplicationBuilder AddOidc(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services
      .AddAuthentication(
        options =>
        {
          options.DefaultScheme =
            CookieAuthenticationDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme =
            OpenIdConnectDefaults.AuthenticationScheme;
        })
      .AddCookie()
      .AddOpenIdConnect(
        options =>
        {
          var connectionString = ConfigureOzdsUsersOptions
            .OidcConnectionString(builder.Configuration);
          var requireHttpsMetadata = ConfigureOzdsUsersOptions
            .RequireHttpsMetadata(builder.Configuration);
          var authLogoutSubpath = ConfigureOzdsUsersOptions
            .AuthLogoutSubpath(builder.Configuration);
          var idKey = ConfigureOzdsUsersOptions
            .IdKey(builder.Configuration);
          var idClaim = ConfigureOzdsUsersOptions
            .IdClaim(builder.Configuration);
          var signInCallbackSubpath = ConfigureOzdsUsersOptions
            .SignInCallbackSubpath(builder.Configuration);
          var signOutCallbackSubpath = ConfigureOzdsUsersOptions
            .SignOutCallbackSubpath(builder.Configuration);

          options.Authority = connectionString.Authority;
          options.RequireHttpsMetadata = requireHttpsMetadata ?? !builder.Environment.IsDevelopment();
          options.ClientId = connectionString.ClientId;
          options.ClientSecret = connectionString.ClientSecret;
          options.ResponseType = OpenIdConnectResponseType.Code;
          options.Scope.Clear();
          options.Scope.Add("openid");
          options.Scope.Add("profile");
          options.Scope.Add("email");
          options.Scope.Add("offline_access");

          options.CallbackPath = $"/{signInCallbackSubpath}";
          options.SignedOutCallbackPath = $"/{signOutCallbackSubpath}";
          var events = new OpenIdConnectEvents();
          if (authLogoutSubpath is { } logoutSubpath)
          {
            events.OnRedirectToIdentityProviderForSignOut = context =>
            {
              context.ProtocolMessage.IssuerAddress =
                $"{connectionString.Authority}/{logoutSubpath}";
              return Task.CompletedTask;
            };
          }

          if (builder.Environment.IsDevelopment())
          {
            events.OnUserInformationReceived = context =>
            {
              var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<OpenIdConnectEvents>>();
              logger.LogDebug(
                "User info received from user info endpoint: {User}",
                context.User.ToString());
              return Task.CompletedTask;
            };
            events.OnTokenValidated = context =>
            {
              var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<OpenIdConnectEvents>>();
              logger.LogDebug("Token validated. Claims from ID token:");
              foreach (var claim in context.Principal?.Claims ?? [])
              {
                logger.LogDebug(
                  "Type: {Type}, Value: {Value}",
                  claim.Type,
                  claim.Value
                );
              }

              return Task.CompletedTask;
            };
            events.OnAuthenticationFailed = context =>
            {
              var logger = context.HttpContext.RequestServices
                .GetRequiredService<ILogger<OpenIdConnectEvents>>();
              logger.LogDebug(
                context.Exception,
                "Authentication failed");
              return Task.CompletedTask;
            };
          }

          options.Events = events;

          if (builder.Environment.IsDevelopment())
          {
            options.RequireHttpsMetadata = false;
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
            options.BackchannelHttpHandler = new HttpClientHandler
            {
              ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections
          }

          options.MapInboundClaims = true;
          options.SaveTokens = true;
          options.GetClaimsFromUserInfoEndpoint = true;
          options.ClaimActions.MapJsonKey(idClaim, idKey);
        });

    builder.Services.AddAuthorization();

    return builder;
  }
}
