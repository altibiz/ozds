using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntitiesChangedPublisher
  : IPublisher<IEntitiesChangedSubscriber, EntitiesChangedEventArgs>
{
}
