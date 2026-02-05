using Microsoft.Extensions.Caching.Memory;
using Ozds.Caching.Cache.Base;
using Ozds.Caching.Configuration;

namespace Ozds.Caching.Cache.Implementations;

public class InMemoryCache<TValue>(
  IServiceProvider services,
  IMemoryCache cache
) : Cache<TValue>(services)
  where TValue : notnull
{
  protected override Task Create(
    CacheEntryConfiguration entryConfiguration,
    string key,
    string value,
    CancellationToken cancellationToken
  )
  {
    var entry = new MemoryCacheEntryOptions
    {
      AbsoluteExpirationRelativeToNow = entryConfiguration.HardTtl,
      SlidingExpiration = entryConfiguration.SoftTtl,
    };
    cache.Set(key, value, entry);
    return Task.CompletedTask;
  }

  protected override Task<string?> Read(
    string key,
    CancellationToken cancellationToken
  )
  {
    return Task.FromResult(cache.Get<string>(key));
  }

  protected override Task<string?> Delete(
    string key,
    CancellationToken cancellationToken
  )
  {
    var deleted = cache.Get<string>(key);
    cache.Remove(key);
    return Task.FromResult(deleted);
  }
}
