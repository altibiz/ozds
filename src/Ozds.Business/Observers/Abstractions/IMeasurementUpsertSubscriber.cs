using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  IMeasurementUpsertSubscriber : ISubscriber<IMeasurementUpsertPublisher>
{
  public void SubscribeUpsert(
    EventHandler<MeasurementUpsertEventArgs> handler);

  public void UnsubscribeUpsert(
    EventHandler<MeasurementUpsertEventArgs> handler);
}
