using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  IMeasurementFlushSubscriber : ISubscriber<IMeasurementFlushPublisher>
{
  public void SubscribeFlush(
    EventHandler<MeasurementFlushEventArgs> handler);

  public void UnsubscribeFlush(
    EventHandler<MeasurementFlushEventArgs> handler);
}
