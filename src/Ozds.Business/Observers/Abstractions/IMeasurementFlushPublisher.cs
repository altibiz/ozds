using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IMeasurementFlushPublisher
  : IPublisher<IMeasurementFlushSubscriber>
{
  public void PublishFlush(MeasurementFlushEventArgs eventArgs);
}
