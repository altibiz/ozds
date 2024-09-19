using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  INotificationCreatedSubscriber : ISubscriber<INotificationCreatedPublisher>
{
  public void SubscribeCreated(
    EventHandler<NotificationCreatedEventArgs> handler);

  public void UnsubscribeCreated(
    EventHandler<NotificationCreatedEventArgs> handler);
}
