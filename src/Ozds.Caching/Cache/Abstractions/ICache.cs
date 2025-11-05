using Ozds.Caching.Configuration;

namespace Ozds.Caching.Cache.Abstractions;

public interface ICache
{
  public Task CreateObject(
    string key,
    object value,
    CancellationToken cancellationToken);

  public Task<object?> ReadObject(
    string key,
    CancellationToken cancellationToken);

  public Task DeleteObject(
    string key,
    CancellationToken cancellationToken);
}

public interface IConfigurableCache : ICache
{
  public void Configure(CacheConfiguration configuration);
}

public interface ICache<TValue> : ICache
{
  Task<TValue?> Read(
    string key,
    CancellationToken cancellationToken);

  Task Create(
    string key,
    TValue value,
    CancellationToken cancellationToken);

  Task Delete(
    string key,
    CancellationToken cancellationToken);

  async Task<object?> ICache.ReadObject(
    string key,
    CancellationToken cancellationToken)
  {
    return await Read(key, cancellationToken);
  }

  async Task ICache.CreateObject(
    string key,
    object value,
    CancellationToken cancellationToken)
  {
    await Create(key, (TValue)value, cancellationToken);
  }

  async Task ICache.DeleteObject(
    string key,
    CancellationToken cancellationToken)
  {
    await Delete(key, cancellationToken);
  }
}

public interface IConfigurableCache<TValue>
  : ICache<TValue>, IConfigurableCache
{
}
