using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class NotificationCreatedObserver : INotificationCreatedPublisher,
  INotificationCreatedSubscriber
{
  public void PublishCreated(NotificationCreatedEventArgs eventArgs)
  {
    OnCreated?.Invoke(this, eventArgs);
  }

  public void SubscribeCreated(
    EventHandler<NotificationCreatedEventArgs> handler)
  {
    OnCreated += handler;
  }

  public void UnsubscribeCreated(
    EventHandler<NotificationCreatedEventArgs> handler)
  {
    OnCreated -= handler;
  }

  private event EventHandler<NotificationCreatedEventArgs>? OnCreated;
}
