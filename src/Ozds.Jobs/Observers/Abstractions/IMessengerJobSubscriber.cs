namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobSubscriber : IObserver
{
  public EventHandler<string>? OnInactivity { get; set; }
}
