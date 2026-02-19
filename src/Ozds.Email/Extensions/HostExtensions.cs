using MailKit.Net.Smtp;
using Ozds.Email.Options;
using Ozds.Email.Sender;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Email.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsEmail(
    this IHostApplicationBuilder builder
  )
  {
    builder.AddOptions();
    builder.AddSender();
    builder.AddMail();
    return builder;
  }

  private static IHostApplicationBuilder AddOptions(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.ConfigureOptions<ConfigureOzdsEmailOptions>();
    return builder;
  }

  private static IHostApplicationBuilder AddSender(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddTransient<IEmailSender, SmtpSender>();
    return builder;
  }

  private static void AddMail(this IHostApplicationBuilder builder)
  {
    builder.Services.AddTransient<ISmtpClient, SmtpClient>();
  }
}
