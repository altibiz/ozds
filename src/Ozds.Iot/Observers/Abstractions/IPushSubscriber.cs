using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Iot.Observers.Abstractions;

public interface IPushSubscriber : ISubscriber<IPushPublisher, PushEventArgs>
{
}
