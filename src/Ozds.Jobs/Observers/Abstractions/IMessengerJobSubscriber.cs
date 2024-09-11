using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IMessengerJobSubscriber : ISubscriber<IMessengerJobPublisher>
{
  public void SubscribeInactivity(
    EventHandler<MessengerInactivityEventArgs> handler);

  public void UnsubscribeInactivity(
    EventHandler<MessengerInactivityEventArgs> handler);
}
