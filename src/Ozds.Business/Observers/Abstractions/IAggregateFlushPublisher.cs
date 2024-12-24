using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IAggregateFlushPublisher
  : IPublisher<IAggregateFlushSubscriber>
{
  public void PublishFlush(AggregateFlushEventArgs eventArgs);
}
