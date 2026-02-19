using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Observers.EventArgs;

public class NotificationRecipientsCreatedEventArgs : System.EventArgs
{
  public required IReadOnlyList<NotificationRecipientsCreatedEventArgsNotificationRecipients> NotificationRecipients { get; init; }
}

public class NotificationRecipientsCreatedEventArgsNotificationRecipients
{
  public required INotification Notification { get; init; }

  public required IReadOnlyList<NotificationRecipientModel> Recipients { get; init; }
}
