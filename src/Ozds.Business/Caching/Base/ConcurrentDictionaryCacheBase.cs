using System.Collections.Concurrent;
using Ozds.Business.Caching.Abstractions;

namespace Ozds.Business.Caching.Base;

public abstract class ConcurrentDictionaryCacheBase<TKey, TValue> : ICache
  where TKey : notnull
{
  private readonly ConcurrentDictionary<TKey, TValue> cache = new();

  public async Task<TValue?> GetAsync(
    TKey key,
    CancellationToken cancellationToken
  )
  {
    if (cache.TryGetValue(key, out var value))
    {
      return value;
    }

    value = await GetValueFromDataSourceAsync(key, cancellationToken);
    if (value is null)
    {
      return default;
    }

    cache.TryAdd(key, value);
    return value;
  }

  public async Task<IReadOnlyCollection<TValue>> GetAsync(
    IEnumerable<TKey> keys,
    CancellationToken cancellationToken
  )
  {
    var keysList = keys.ToList();

    var values = keysList
      .Select(
        key =>
        {
          if (cache.TryGetValue(key, out var value))
          {
            return value;
          }

          return default;
        })
      .ToList();

    if (values.TrueForAll(x => x is not null))
    {
      return values!;
    }

    var missing = await GetValuesFromDataSourceAsync(
      keysList
        .Zip(values)
        .Select(
          x =>
          {
            var (key, value) = x;
            return new
            {
              Key = key,
              Value = value
            };
          })
        .Where(x => x.Value is null)
        .Select(x => x.Key)
        .ToList(),
      cancellationToken);

    var result = new List<TValue>(keysList.Count);
    var missingEnumerator = missing.GetEnumerator();
    foreach (var index in values.Select((x, i) => i))
    {
      if (values[index] is { } value)
      {
        cache.TryAdd(keysList[index], value);
        result.Add(value);
        continue;
      }

      if (missingEnumerator.MoveNext()
        && missingEnumerator.Current is { } current)
      {
        cache.TryAdd(keysList[index], current);
        result.Add(current);
      }
    }

    return result;
  }

  public TValue? TryUpdate(
    TKey key,
    TValue value
  )
  {
    if (!cache.TryGetValue(key, out var old))
    {
      return default;
    }

    if (!cache.TryUpdate(key, value, old))
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

    if (!cache.TryGetValue(key, out var old))
    {
      return default;
    }

    if (!cache.TryUpdate(key, value, old))
    {
      return default;
    }

    return old;
  }

  public async Task<IReadOnlyCollection<TValue?>> TryUpdateAsync(
    IEnumerable<TValue> values,
    CancellationToken cancellationToken
  )
  {
    var valueList = values.ToList();

    var keys = await GetKeysFromDataSourceAsync(valueList, cancellationToken);

    return keys
      .Zip(valueList)
      .Select(
        x =>
        {
          var (key, value) = x;
          return new
          {
            Key = key,
            Value = value
          };
        })
      .Select(
        x =>
        {
          if (x.Key is null)
          {
            return default;
          }

          if (!cache.TryGetValue(x.Key, out var old))
          {
            return default;
          }

          if (!cache.TryUpdate(x.Key, x.Value, old))
          {
            return default;
          }

          return old;
        })
      .ToList();
  }

  public TValue? TryRemove(
    TKey key
  )
  {
    if (!cache.TryRemove(key, out var old))
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

    if (!cache.TryRemove(key, out var old))
    {
      return default;
    }

    return old;
  }

  public async Task<IReadOnlyCollection<TValue?>> TryRemoveAsync(
    IReadOnlyCollection<TValue> values,
    CancellationToken cancellationToken
  )
  {
    var keys = await GetKeysFromDataSourceAsync(values, cancellationToken);

    return keys
      .Zip(values)
      .Select(
        x =>
        {
          var (key, value) = x;
          return new
          {
            Key = key,
            Value = value
          };
        })
      .Select(
        x =>
        {
          if (x.Key is null)
          {
            return default;
          }

          if (!cache.TryRemove(x.Key, out var old))
          {
            return default;
          }

          return old;
        })
      .ToList();
  }

  protected abstract Task<IReadOnlyCollection<TKey?>>
    GetKeysFromDataSourceAsync(
      IReadOnlyCollection<TValue> values,
      CancellationToken cancellationToken
    );

  protected abstract Task<IReadOnlyCollection<TValue?>>
    GetValuesFromDataSourceAsync(
      IReadOnlyCollection<TKey> keys,
      CancellationToken cancellationToken
    );

  protected abstract Task<TKey?> GetKeyFromDataSourceAsync(
    TValue value,
    CancellationToken cancellationToken
  );

  protected abstract Task<TValue?> GetValueFromDataSourceAsync(
    TKey key,
    CancellationToken cancellationToken
  );
}
