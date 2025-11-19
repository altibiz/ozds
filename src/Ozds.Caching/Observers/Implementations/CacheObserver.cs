using Ozds.Caching.Observers.Abstractions;
using Ozds.Caching.Observers.Base;
using Ozds.Caching.Observers.EventArgs;

namespace Ozds.Iot.Observers.Implementations;

public class CacheObserver :
  Observer<CacheEventArgs>,
  ICachePublisher,
  ICacheSubscriber
{
}
