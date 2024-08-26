namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobPublisher : IObserver
{
  public void PublishInactivity(string id);
}
