using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IMeasurementSubscriber : ISubscriber<IMeasurementPublisher>
{
  public void SubscribePush(
    EventHandler<MeasurementPushEventArgs> handler);

  public void UnsubscribePush(
    EventHandler<MeasurementPushEventArgs> handler);

  public void SubscribeUpsert(
    EventHandler<MeasurementUpsertEventArgs> handler);

  public void UnsubscribeUpsert(
    EventHandler<MeasurementUpsertEventArgs> handler);
}
