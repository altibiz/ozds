using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobPublisher : IPublisher<IMessengerJobSubscriber>
{
  public void PublishInactivity(MessengerInactivityEventArgs id);
}
