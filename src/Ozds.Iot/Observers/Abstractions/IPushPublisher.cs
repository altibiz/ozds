using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Iot.Observers.Abstractions;

public interface IPushPublisher : IPublisher<IPushSubscriber>
{
  public void PublishPush(PushEventArgs eventArgs);
}
