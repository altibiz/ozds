using Ozds.Caching.Observers.EventArgs;

namespace Ozds.Caching.Observers.Abstractions;

public interface ICacheSubscriber
  : ISubscriber<ICachePublisher, CacheEventArgs> { }
