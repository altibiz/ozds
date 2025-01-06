using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntitiesChangingPublisher
  : IPublisher<IEntitiesChangingSubscriber, EntitiesChangingEventArgs>
{
}
