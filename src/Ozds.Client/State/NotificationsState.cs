using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.State;

public record NotificationsState(
  List<INotification> Notifications,
  Action<INotification> MarkNotificationAsSeen,
  Action<INotification> AddNotification
);
