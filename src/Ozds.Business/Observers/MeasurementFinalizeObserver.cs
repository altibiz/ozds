using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class MeasurementFinalizeObserver : IMeasurementFinalizePublisher,
  IMeasurementFinalizeSubscriber
{
  public void PublishFinalize(MeasurementFinalizeEventArgs eventArgs)
  {
    OnUpsert?.Invoke(this, eventArgs);
  }

  public void SubscribeFinalize(
    EventHandler<MeasurementFinalizeEventArgs> handler)
  {
    OnUpsert += handler;
  }

  public void UnsubscribeFinalize(
    EventHandler<MeasurementFinalizeEventArgs> handler)
  {
    OnUpsert -= handler;
  }

  private event EventHandler<MeasurementFinalizeEventArgs>? OnUpsert;
}
