using System.Collections.Concurrent;
using Ozds.Business.Caching.Abstractions;

namespace Ozds.Business.Caching.Base;

public abstract class ConcurrentDictionaryCacheBase<TKey, TValue> : ICache
  where TKey : notnull
{
  protected ConcurrentDictionary<TKey, TValue> Cache { get; } = new();

  public async Task<TValue?> GetAsync(
    TKey key,
    CancellationToken cancellationToken
  )
  {
    if (Cache.TryGetValue(key, out var value))
    {
      return value;
    }

    value = await GetValueFromDataSourceAsync(key, cancellationToken);
    if (value is null)
    {
      return default;
    }

    Cache.TryAdd(key, value);
    return value;
  }

  public TValue? TryUpdate(
    TKey key,
    TValue value
  )
  {
    if (!Cache.TryGetValue(key, out var old))
    {
      return default;
    }

    if (!Cache.TryUpdate(key, value, old))
    {
      return default;
    }

    return old;
  }

  public async Task<TValue?> TryUpdateAsync(
    TValue value,
    CancellationToken cancellationToken
  )
  {
    var key = await GetKeyFromDataSourceAsync(value, cancellationToken);
    if (key is null)
    {
      return default;
    }

    if (!Cache.TryGetValue(key, out var old))
    {
      return default;
    }

    if (!Cache.TryUpdate(key, value, old))
    {
      return default;
    }

    return old;
  }

  public TValue? TryRemove(
    TKey key
  )
  {
    if (!Cache.TryRemove(key, out var old))
    {
      return default;
    }

    return old;
  }

  public async Task<TValue?> TryRemoveAsync(
    TValue value,
    CancellationToken cancellationToken
  )
  {
    var key = await GetKeyFromDataSourceAsync(value, cancellationToken);
    if (key is null)
    {
      return default;
    }

    if (!Cache.TryRemove(key, out var old))
    {
      return default;
    }

    return old;
  }

  protected abstract Task<TKey?> GetKeyFromDataSourceAsync(
    TValue value,
    CancellationToken cancellationToken
  );

  protected abstract Task<TValue?> GetValueFromDataSourceAsync(
    TKey key,
    CancellationToken cancellationToken
  );
}
