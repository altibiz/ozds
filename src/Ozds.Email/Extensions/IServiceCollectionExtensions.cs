using MailKit.Net.Smtp;
using Ozds.Email.Options;
using Ozds.Email.Sender;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Email.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsEmail(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.AddOptions(builder);
    services.AddSender();
    services.AddMailKit(builder);
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    services.Configure<OzdsEmailOptions>(
      builder.Configuration.GetSection("Ozds:Email"));
    return services;
  }

  private static IServiceCollection AddSender(
    this IServiceCollection services
  )
  {
    services.AddTransient<IEmailSender, SmtpSender>();
    return services;
  }

  private static void AddMailKit(
    this IServiceCollection services,
    IHostApplicationBuilder builder
  )
  {
    _ = builder.Configuration
        .GetSection("Ozds:Email")
        .Get<OzdsEmailOptions>()
      ?? throw new InvalidOperationException(
        "Missing Ozds:Email configuration");

    services.AddTransient<ISmtpClient, SmtpClient>();
  }
}
