using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IMeasurementPublisher : IPublisher<IMeasurementSubscriber>
{
  public void PublishPush(MeasurementPushEventArgs eventArgs);

  public void PublishUpsert(MeasurementUpsertEventArgs eventArgs);
}
