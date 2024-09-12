using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class MeasurementUpsertObserver : IMeasurementUpsertPublisher,
  IMeasurementUpsertSubscriber
{
  public void PublishUpsert(MeasurementUpsertEventArgs eventArgs)
  {
    OnUpsert?.Invoke(this, eventArgs);
  }

  public void SubscribeUpsert(
    EventHandler<MeasurementUpsertEventArgs> handler)
  {
    OnUpsert += handler;
  }

  public void UnsubscribeUpsert(
    EventHandler<MeasurementUpsertEventArgs> handler)
  {
    OnUpsert -= handler;
  }

  private event EventHandler<MeasurementUpsertEventArgs>? OnUpsert;
}
