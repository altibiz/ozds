using MailKit.Net.Smtp;
using MimeKit;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Notifications.Abstractions;

namespace Ozds.Business.Notifications;

public class NotificationSender(
  ISmtpClient client,
  IConfiguration configuration
) : INotificationSender
{
  public async Task SendAsync(
    INotification notification,
    IEnumerable<RepresentativeModel> recipients)
  {
    var notificationConfiguration = configuration
      .GetSection("Ozds:Notifications");
    var emailConfiguration = notificationConfiguration
      .GetSection("Email");
    var smtpConfiguration = emailConfiguration
      .GetSection("Smtp");

    var host = smtpConfiguration.GetValue<string>("Host")
      ?? throw new InvalidOperationException("Email host is not configured.");
    var port = smtpConfiguration.GetValue<int?>("Port")
      ?? throw new InvalidOperationException("Email port is not configured.");
    var username = smtpConfiguration.GetValue<string>("Username")
      ?? throw new InvalidOperationException("Email username is not configured.");
    var password = smtpConfiguration.GetValue<string>("Password")
      ?? throw new InvalidOperationException("Email password is not configured.");
    var ssl = smtpConfiguration.GetValue<bool?>("Ssl")
      ?? throw new InvalidOperationException("Email ssl is not configured.");

    var from = emailConfiguration.GetValue<string>("From")
      ?? throw new InvalidOperationException("Email from is not configured.");
    var address = emailConfiguration.GetValue<string>("Address")
      ?? throw new InvalidOperationException("Email address is not configured.");

    var messages = new List<MimeMessage>();
    var fromAddress = new MailboxAddress(from, address);
    var subject = $"[OZDS - {notification.Topic.ToTitle()}]: {notification.Title}";
    var body = new BodyBuilder() { HtmlBody = notification.Content, }.ToMessageBody();

    foreach (var recipient in recipients.Select(r => r.PhysicalPerson))
    {
      var message = new MimeMessage();
      message.From.Add(fromAddress);
      message.Subject = subject;
      message.Body = body;

      message.To.Add(new MailboxAddress(recipient.Name, recipient.Email));

      messages.Add(message);
    }

    await client.ConnectAsync(host, port, ssl);
    await client.AuthenticateAsync(username, password);

    foreach (var message in messages)
    {
      await client.SendAsync(message);
    }

    await client.DisconnectAsync(true);
  }
}
