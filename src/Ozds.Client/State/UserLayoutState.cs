using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.State;

public record UserLayoutState(
  bool Seen,
  List<INotification> Notifications,
  Action<INotification> MarkNotificationAsSeen,
  Action<bool> SetSeen
);
