using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntitiesChangedSubscriber
  : ISubscriber<IEntitiesChangedPublisher, EntitiesChangedEventArgs>
{
}
