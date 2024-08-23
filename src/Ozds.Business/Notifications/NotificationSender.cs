using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Notifications.Abstractions;
using Ozds.Data;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Business.Notifications;

public class NotificationSender(
  OzdsDataDbContext context,
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
      .Select(recipient => new NotificationRepresentativeModel()
      {
        NotificationId = notification.Id,
        RepresentativeId = recipient.Id
      })
      .Select(converter.ToEntity));
    await context.SaveChangesAsync();

    await sender.SendBulkAsync(recipients.Select(
      recipient => new EmailMessage(
        recipient.PhysicalPerson.Name,
        recipient.PhysicalPerson.Email,
        $"[OZDS - {notification.Topic.ToTitle()}]: {notification.Title}",
        notification.Content
      )
    ));
  }
}
