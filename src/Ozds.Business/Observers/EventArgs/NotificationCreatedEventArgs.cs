using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Observers.EventArgs;

public class NotificationCreatedEventArgs : System.EventArgs
{
  public INotification Notification { get; set; } = default!;
}
