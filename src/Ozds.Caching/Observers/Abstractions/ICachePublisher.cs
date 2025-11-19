using Ozds.Caching.Observers.EventArgs;

namespace Ozds.Caching.Observers.Abstractions;

public interface ICachePublisher : IPublisher<ICacheSubscriber, CacheEventArgs>
{
}
