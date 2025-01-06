using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.Base;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Iot.Observers;

public class PushObserver :
  Observer<PushEventArgs>,
  IPushPublisher,
  IPushSubscriber
{
}
