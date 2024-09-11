using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Ozds.Email.Options;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Email.Sender;

public class SmtpSender(
  ISmtpClient client,
  IOptions<OzdsEmailOptions> options
) : IEmailSender
{
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
      options.Value.Smtp.Host,
      options.Value.Smtp.Port,
      options.Value.Smtp.Ssl
    );
    client.Authenticate(
      options.Value.Smtp.Username,
      options.Value.Smtp.Password
    );
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
      options.Value.Smtp.Host, options.Value.Smtp.Port, options.Value.Smtp.Ssl);
    await client.AuthenticateAsync(
      options.Value.Smtp.Username, options.Value.Smtp.Password);
    await client.SendAsync(mimeMessage);
    await client.DisconnectAsync(true);
  }

  public void SendBulk(IEnumerable<EmailMessage> messages)
  {
    var mimeMessages = messages.Select(
      m =>
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
      options.Value.Smtp.Host,
      options.Value.Smtp.Port,
      options.Value.Smtp.Ssl
    );
    client.Authenticate(
      options.Value.Smtp.Username,
      options.Value.Smtp.Password
    );
    foreach (var mimeMessage in mimeMessages)
    {
      client.Send(mimeMessage);
    }

    client.Disconnect(true);
  }

  public async Task SendBulkAsync(IEnumerable<EmailMessage> messages)
  {
    var mimeMessages = messages.Select(
      m =>
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
      options.Value.Smtp.Host, options.Value.Smtp.Port, options.Value.Smtp.Ssl);
    await client.AuthenticateAsync(
      options.Value.Smtp.Username, options.Value.Smtp.Password);
    foreach (var mimeMessage in mimeMessages)
    {
      await client.SendAsync(mimeMessage);
    }

    await client.DisconnectAsync(true);
  }
}
