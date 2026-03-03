using Altibiz.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ozds.Users.Context;
using Ozds.Users.Entities;
using Ozds.Users.Mutations.Abstractions;
using Ozds.Users.Options;
using Ozds.Users.Queries.Abstractions;

namespace Ozds.Users.Extensions;

public static class HostExtensions
{
  public const string AuthenticationScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;

  public static IHostApplicationBuilder AddOzdsUsers(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddQueries();
    builder.AddMutations();
    builder.AddHttp();
    builder.AddDatabase();
    if (ConfigureOzdsUsersOptions.WithAuth(builder.Configuration))
    {
      builder.AddIdentity();
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

  private static void AddDatabase(this IHostApplicationBuilder builder)
  {
    builder.Services.AddDbContext<UsersDbContext>(
      (services, options) =>
      {
        var usersOptions = services
          .GetRequiredService<IOptions<OzdsUsersOptions>>()
          .Value;

        options
          .UseNpgsql(
            usersOptions.ConnectionString,
            x =>
            {
              x.MigrationsAssembly(
                typeof(UsersDbContext).Assembly.GetName().Name
              );
              x.MigrationsHistoryTable($"__Ozds{nameof(UsersDbContext)}");
            }
          )
          .UseSnakeCaseNamingConvention();
      }
    );
  }

  private static IHostApplicationBuilder AddIdentity(
    this IHostApplicationBuilder builder
  )
  {
    builder
      .Services.AddIdentity<OzdsUser, IdentityRole>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        options.User.RequireUniqueEmail = true;

        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
      })
      .AddEntityFrameworkStores<UsersDbContext>()
      .AddDefaultTokenProviders();

    builder.Services.ConfigureApplicationCookie(options =>
    {
      options.Cookie.Name = ".Ozds.Auth";
      options.Cookie.HttpOnly = true;
      options.Cookie.SameSite = SameSiteMode.Lax;

      options.ExpireTimeSpan = TimeSpan.FromDays(30);
      options.SlidingExpiration = true;

      options.LoginPath = "/app/auth/login";
      options.LogoutPath = "/app/auth/logout";
      options.AccessDeniedPath = "/app/auth/access-denied";

      options.Events.OnRedirectToLogin = context =>
      {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
          context.Response.StatusCode =
            StatusCodes.Status401Unauthorized;
          return Task.CompletedTask;
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
      };
    });

    builder.Services.AddAuthorization();

    var configuration = builder.Configuration;

    if (
      configuration["Ozds:Users:Authentication:Google:ClientId"]
        is { Length: > 0 } googleId
    )
    {
      builder
        .Services.AddAuthentication()
        .AddGoogle(options =>
        {
          options.ClientId = googleId;
          options.ClientSecret = configuration.GetValue<string>(
            "Ozds:Users:Authentication:Google:ClientSecret"
          )!;
        });
    }

    if (
      configuration["Ozds:Users:Authentication:Facebook:AppId"]
        is { Length: > 0 } facebookId
    )
    {
      builder
        .Services.AddAuthentication()
        .AddFacebook(options =>
        {
          options.AppId = facebookId;
          options.AppSecret = configuration.GetValue<string>(
            "Ozds:Users:Authentication:Facebook:AppSecret"
          )!;
        });
    }

    if (
      configuration["Ozds:Users:Authentication:Microsoft:ClientId"]
        is { Length: > 0 } microsoftId
    )
    {
      builder
        .Services.AddAuthentication()
        .AddMicrosoftAccount(options =>
        {
          options.ClientId = microsoftId;
          options.ClientSecret = configuration.GetValue<string>(
            "Ozds:Users:Authentication:Microsoft:ClientSecret"
          )!;
        });
    }

    return builder;
  }
}
