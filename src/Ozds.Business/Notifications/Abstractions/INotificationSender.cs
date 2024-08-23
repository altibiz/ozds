using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Notifications.Abstractions;

public record NotificationRecipient(
  string Name,
  string Address
);

public interface INotificationSender
{
  Task SendAsync(
    INotification notification,
    IEnumerable<RepresentativeModel> recipients
  );
}
