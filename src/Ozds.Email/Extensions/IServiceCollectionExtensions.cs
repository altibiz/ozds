using MailKit.Net.Smtp;
using Ozds.Email.Options;
using Ozds.Email.Sender;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Email.Extensions;

public static class IServiceCollectionExtensions
{
  public static IServiceCollection AddOzdsEmail(
    this IServiceCollection services
  )
  {
    services.AddOptions();
    services.AddSender();
    services.AddMail();
    return services;
  }

  private static IServiceCollection AddOptions(
    this IServiceCollection services
  )
  {
    services.ConfigureOptions<ConfigureOzdsEmailOptions>();
    return services;
  }

  private static IServiceCollection AddSender(
    this IServiceCollection services
  )
  {
    services.AddTransient<IEmailSender, SmtpSender>();
    return services;
  }

  private static void AddMail(
    this IServiceCollection services
  )
  {
    services.AddTransient<ISmtpClient, SmtpClient>();
  }
}
