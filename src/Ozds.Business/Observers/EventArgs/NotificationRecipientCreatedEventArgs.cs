using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Observers.EventArgs;

public class NotificationRecipientCreatedEventArgs : System.EventArgs
{
  public required INotification Notification { get; init; }

  public required NotificationRecipientModel Recipient { get; init; }
}
