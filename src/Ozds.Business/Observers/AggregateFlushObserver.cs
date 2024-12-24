using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class AggregateFlushObserver : IAggregateFlushPublisher,
  IAggregateFlushSubscriber
{
  public void PublishFlush(AggregateFlushEventArgs eventArgs)
  {
    OnFlush?.Invoke(this, eventArgs);
  }

  public void SubscribeFlush(
    EventHandler<AggregateFlushEventArgs> handler)
  {
    OnFlush += handler;
  }

  public void UnsubscribeFlush(
    EventHandler<AggregateFlushEventArgs> handler)
  {
    OnFlush -= handler;
  }

  private event EventHandler<AggregateFlushEventArgs>? OnFlush;
}
