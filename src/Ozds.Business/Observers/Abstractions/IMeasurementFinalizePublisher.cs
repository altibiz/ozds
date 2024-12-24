using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  IMeasurementFinalizePublisher : IPublisher<IMeasurementFinalizeSubscriber>
{
  public void PublishFinalize(MeasurementFinalizeEventArgs eventArgs);
}
