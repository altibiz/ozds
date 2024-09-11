using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Iot.Observers;

public class PushObserver : IPushPublisher, IPushSubscriber
{
  public void PublishPush(PushEventArgs eventArgs)
  {
    OnPush?.Invoke(this, eventArgs);
  }

  public void SubscribePush(
    EventHandler<PushEventArgs> handler)
  {
    OnPush += handler;
  }

  public void UnsubscribePush(
    EventHandler<PushEventArgs> handler)
  {
    OnPush -= handler;
  }

  private event EventHandler<PushEventArgs>? OnPush;
}
