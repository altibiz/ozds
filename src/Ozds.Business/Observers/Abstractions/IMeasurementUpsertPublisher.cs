using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IMeasurementUpsertPublisher : IPublisher<IMeasurementUpsertSubscriber>
{
  public void PublishUpsert(MeasurementUpsertEventArgs eventArgs);
}
