using System.Text;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Notifications.Abstractions;
using Ozds.Data.Context;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Business.Notifications;

public class NotificationSender(
  DataDbContext context,
  AgnosticModelEntityConverter converter,
  IEmailSender sender
) : INotificationSender
{
  public async Task SendAsync(
    INotification notification,
    IEnumerable<RepresentativeModel> recipients)
  {
    context.Add(converter.ToEntity(notification));
    context.AddRange(recipients
      .Select(recipient => new NotificationRecipientModel()
      {
        NotificationId = notification.Id,
        RepresentativeId = recipient.Id
      })
      .Select(converter.ToEntity));
    await context.SaveChangesAsync();

    var titleBuilder = new StringBuilder($"[OZDS]: {notification.Title}");
    if (notification.Topics.Count > 0)
    {
      var topics = notification.Topics.Select(x => x.ToTitle());
      titleBuilder.Append(" (");
      titleBuilder.Append(string.Join(", ", topics));
      titleBuilder.Append(")");
    }

    await sender.SendBulkAsync(recipients.Select(
      recipient => new EmailMessage(
        recipient.PhysicalPerson.Name,
        recipient.PhysicalPerson.Email,
        titleBuilder.ToString(),
        notification.Content
      )
    ));
  }
}
