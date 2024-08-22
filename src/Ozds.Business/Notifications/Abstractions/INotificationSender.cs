using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Notifications.Abstractions;

public interface INotificationSender
{
  Task SendAsync(INotification notification);
}
