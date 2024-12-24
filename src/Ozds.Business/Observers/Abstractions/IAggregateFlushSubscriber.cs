using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  IAggregateFlushSubscriber : ISubscriber<IAggregateFlushPublisher>
{
  public void SubscribeFlush(
    EventHandler<AggregateFlushEventArgs> handler);

  public void UnsubscribeFlush(
    EventHandler<AggregateFlushEventArgs> handler);
}
