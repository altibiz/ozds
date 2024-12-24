using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface
  IMeasurementFinalizeSubscriber : ISubscriber<IMeasurementFinalizePublisher>
{
  public void SubscribeFinalize(
    EventHandler<MeasurementFinalizeEventArgs> handler);

  public void UnsubscribeFinalize(
    EventHandler<MeasurementFinalizeEventArgs> handler);
}
