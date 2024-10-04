using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Joins;

namespace Ozds.Business.Observers.EventArgs;

public class NotificationCreatedEventArgs : System.EventArgs
{
  public INotification Notification { get; set; } = default!;

  public NotificationRecipientModel Recipient { get; set; } = default!;
}
