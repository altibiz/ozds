using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  INotificationCreatedPublisher : IPublisher<INotificationCreatedSubscriber>
{
  public void PublishCreated(NotificationCreatedEventArgs eventArgs);
}