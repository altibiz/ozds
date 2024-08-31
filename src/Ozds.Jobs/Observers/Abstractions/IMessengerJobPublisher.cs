namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobPublisher : IPublisher<IMessengerJobSubscriber>
{
  public void PublishInactivity(string id);
}
