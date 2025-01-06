using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.Base;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers;

public class EntitiesChangedObserver :
  Observer<EntitiesChangedEventArgs>,
  IEntitiesChangedPublisher,
  IEntitiesChangedSubscriber
{
}
