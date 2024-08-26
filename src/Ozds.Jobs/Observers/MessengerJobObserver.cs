using Ozds.Jobs.Observers.Abstractions;

namespace Ozds.Jobs.Observers;

public class MessengerJobManager :
  IMessengerJobPublisher,
  IMessengerJobSubscriber
{
  public EventHandler<string>? OnInactivity { get; set; }

  public void PublishInactivity(string id)
  {
    OnInactivity?.Invoke(this, id);
  }
}
