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
    services.Configure<OzdsEmailOptions>(
      builder.Configuration.GetSection("Ozds:Email"));

    services.AddTransient<ISmtpClient, SmtpClient>();

    services.AddTransient<IEmailSender, SmtpSender>();

    return services;
  }

  public static IApplicationBuilder UseOzdsEmail(
    this IApplicationBuilder app,
    IEndpointRouteBuilder endpoints
  )
  {
    return app;
  }
}
