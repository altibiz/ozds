using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  IAggregateFlushSubscriber : ISubscriber<IMeasurementUpsertPublisher>
{
  public void SubscribeFlush(
    EventHandler<AggregateFlushEventArgs> handler);

  public void UnsubscribeFlush(
    EventHandler<AggregateFlushEventArgs> handler);
}
