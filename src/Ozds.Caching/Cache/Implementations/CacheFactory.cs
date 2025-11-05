using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Profiles;

namespace Ozds.Caching.Cache.Implementations;

public class CacheFactory(
  ProfileRegistry registry,
  IServiceProvider services
) : ICacheFactory, IPolicyCacheFactory
{
  ICache<TValue> ICacheFactory.Create<TValue>()
  {
    var configuration = registry.GetConfiguration(typeof(TValue));
    var cache = services.GetRequiredService<IConfigurableCache<TValue>>();
    cache.Configure(configuration);
    return cache;
  }

  ICache ICacheFactory.Create(Type type)
  {
    var configuration = registry.GetConfiguration(type);
    var cache = services
      .GetRequiredService(typeof(IConfigurableCache<>).MakeGenericType(type))
      as IConfigurableCache
      ?? throw new InvalidOperationException(
        $"{typeof(IConfigurableCache<>).MakeGenericType(type)} not found");
    cache.Configure(configuration);
    return cache;
  }

  IPolicyCache IPolicyCacheFactory.Create()
  {
    var cache = services.GetRequiredService<IPolicyCache>();
    return cache;
  }
}
