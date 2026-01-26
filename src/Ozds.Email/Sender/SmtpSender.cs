using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Ozds.Email.Options;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Email.Sender;

public class SmtpSender(
  ISmtpClient client,
  IOptions<OzdsEmailOptions> options,
  ILogger<SmtpSender> logger
) : IEmailSender
{
  private readonly OzdsEmailParsedSmtpConnectionString connectionString =
    new(options.Value.Smtp.ConnectionString);

  public void Send(EmailMessage message)
  {
    var mimeMessage = new MimeMessage();
    mimeMessage.From.Add(
      new MailboxAddress(options.Value.From.Name, options.Value.From.Address));
    mimeMessage.To.Add(new MailboxAddress(message.Name, message.Address));
    mimeMessage.Subject = message.Subject;
    mimeMessage.Body =
      new BodyBuilder { HtmlBody = message.Content }.ToMessageBody();

    client.Connect(
      connectionString.Host, connectionString.Port, connectionString.Ssl);
    client.Authenticate(connectionString.User, connectionString.Password);
    client.Send(mimeMessage);
    client.Disconnect(true);
  }

  public async Task SendAsync(EmailMessage message)
  {
    var mimeMessage = new MimeMessage();
    mimeMessage.From.Add(
      new MailboxAddress(options.Value.From.Name, options.Value.From.Address));
    mimeMessage.To.Add(new MailboxAddress(message.Name, message.Address));
    mimeMessage.Subject = message.Subject;
    mimeMessage.Body =
      new BodyBuilder { HtmlBody = message.Content }.ToMessageBody();

    await client.ConnectAsync(
      connectionString.Host, connectionString.Port, connectionString.Ssl);
    await client.AuthenticateAsync(
      connectionString.User, connectionString.Password);
    await client.SendAsync(mimeMessage);
    await client.DisconnectAsync(true);
  }

  public void SendBulk(IEnumerable<EmailMessage> messages)
  {
    var mimeMessages = messages.Select(m =>
    {
      var mimeMessage = new MimeMessage();
      mimeMessage.From.Add(
        new MailboxAddress(
          options.Value.From.Name, options.Value.From.Address));
      mimeMessage.To.Add(new MailboxAddress(m.Name, m.Address));
      mimeMessage.Subject = m.Subject;
      mimeMessage.Body =
        new BodyBuilder { HtmlBody = m.Content }.ToMessageBody();

      return mimeMessage;
    });

    client.Connect(
      connectionString.Host, connectionString.Port, connectionString.Ssl);
    client.Authenticate(connectionString.User, connectionString.Password);
    foreach (var mimeMessage in mimeMessages)
    {
      client.Send(mimeMessage);
    }

    client.Disconnect(true);
  }

  public async Task SendBulkAsync(IEnumerable<EmailMessage> messages)
  {
    var mimeMessages = messages.Select(m =>
    {
      var mimeMessage = new MimeMessage();
      mimeMessage.From.Add(
        new MailboxAddress(
          options.Value.From.Name, options.Value.From.Address));
      mimeMessage.To.Add(new MailboxAddress(m.Name, m.Address));
      mimeMessage.Subject = m.Subject;
      mimeMessage.Body =
        new BodyBuilder { HtmlBody = m.Content }.ToMessageBody();

      return mimeMessage;
    });

    await client.ConnectAsync(
      connectionString.Host, connectionString.Port, connectionString.Ssl);
    await client.AuthenticateAsync(
      connectionString.User, connectionString.Password);
    foreach (var mimeMessage in mimeMessages)
    {
      var response = await client.SendAsync(mimeMessage);
      logger.LogDebug("Response {Response}", response);
    }

    await client.DisconnectAsync(true);
  }
}
