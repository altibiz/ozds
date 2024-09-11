using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers;

public class MessengerJobManager :
  IMessengerJobPublisher,
  IMessengerJobSubscriber
{
  private EventHandler<MessengerInactivityEventArgs>? OnInactivity { get; set; }

  public void PublishInactivity(MessengerInactivityEventArgs id)
  {
    OnInactivity?.Invoke(this, id);
  }

  public void SubscribeInactivity(EventHandler<MessengerInactivityEventArgs> handler)
  {
    OnInactivity += handler;
  }

  public void UnsubscribeInactivity(EventHandler<MessengerInactivityEventArgs> handler)
  {
    OnInactivity -= handler;
  }
}
