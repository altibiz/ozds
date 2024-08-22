using MailKit.Net.Smtp;
using MimeKit;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Notifications.Abstractions;

namespace Ozds.Business.Notifications;

public class NotificationSender(
  ISmtpClient client
) : INotificationSender
{
  public Task SendAsync(INotification notification)
  {
    var message = new MimeMessage();
    message.From.Add(new MailboxAddress("AltiBiz", "noreply@altibiz.com"));

    message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
    message.Subject = "How you doin'?";

    var bodyBuilder = new BodyBuilder()
    {
      HtmlBody = "<b>This is some html text</b>",
      TextBody = "This is some plain text"
    };

    message.Body = bodyBuilder.ToMessageBody();

    client.Connect("smtp.friends.com", 587, false);
    client.Authenticate("joey", "password");
    client.Send(message);
    client.Disconnect(true);
  }
}
