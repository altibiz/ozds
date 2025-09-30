namespace Ozds.Business.Caching.Base;

public abstract class BatchedConcurrentDictionaryCacheBase<TKey, TValue>
  : ConcurrentDictionaryCacheBase<TKey, TValue>
  where TKey : notnull
{
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
          if (Cache.TryGetValue(key, out var value))
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
        Cache.TryAdd(keysList[index], value);
        result.Add(value);
        continue;
      }

      if (missingEnumerator.MoveNext()
        && missingEnumerator.Current is { } current)
      {
        Cache.TryAdd(keysList[index], current);
        result.Add(current);
      }
    }

    return result;
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

          if (!Cache.TryGetValue(x.Key, out var old))
          {
            return default;
          }

          if (!Cache.TryUpdate(x.Key, x.Value, old))
          {
            return default;
          }

          return old;
        })
      .ToList();
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

          if (!Cache.TryRemove(x.Key, out var old))
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
}
