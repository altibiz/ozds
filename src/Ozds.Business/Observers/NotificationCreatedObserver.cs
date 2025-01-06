using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class NotificationCreatedObserver :
  Observer<NotificationCreatedEventArgs>,
  INotificationCreatedPublisher,
  INotificationCreatedSubscriber
{
}
