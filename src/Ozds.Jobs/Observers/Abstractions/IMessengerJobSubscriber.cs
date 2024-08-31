namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobSubscriber : ISubscriber<IMessengerJobPublisher>
{
  public EventHandler<string>? OnInactivity { get; set; }
}
