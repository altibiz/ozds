using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.Base;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Implementations;

public class EntitiesChangingObserver :
  Observer<EntitiesChangingEventArgs>,
  IEntitiesChangingPublisher,
  IEntitiesChangingSubscriber
{
}
