using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class MeasurementFlushObserver : IMeasurementFlushPublisher,
  IMeasurementFlushSubscriber
{
  public void PublishFlush(MeasurementFlushEventArgs eventArgs)
  {
    OnFlush?.Invoke(this, eventArgs);
  }

  public void SubscribeFlush(
    EventHandler<MeasurementFlushEventArgs> handler)
  {
    OnFlush += handler;
  }

  public void UnsubscribeFlush(
    EventHandler<MeasurementFlushEventArgs> handler)
  {
    OnFlush -= handler;
  }

  private event EventHandler<MeasurementFlushEventArgs>? OnFlush;
}
