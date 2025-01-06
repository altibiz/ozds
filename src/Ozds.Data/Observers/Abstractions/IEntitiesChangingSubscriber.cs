using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntitiesChangingSubscriber
  : ISubscriber<IEntitiesChangingPublisher, EntitiesChangingEventArgs>
{
}
