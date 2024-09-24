using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Iot.Observers.Abstractions;

public interface IPushSubscriber : ISubscriber<IPushPublisher>
{
  public void SubscribePush(
    EventHandler<PushEventArgs> handler);

  public void UnsubscribePush(
    EventHandler<PushEventArgs> handler);
}
