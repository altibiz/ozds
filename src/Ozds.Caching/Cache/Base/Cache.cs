using System.Text.Json;
using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Configuration;
using Ozds.Caching.Observers.Abstractions;
using Ozds.Caching.Observers.EventArgs;
using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Profiles;

namespace Ozds.Caching.Cache.Base;

public abstract class Cache<TValue>(
  IServiceProvider services
) : IConfigurableCache<TValue>, IPolicyCache
  where TValue : notnull
{
  private readonly ICachePublisher publisher =
    services.GetRequiredService<ICachePublisher>();

  private readonly ProfileRegistry registry =
    services.GetRequiredService<ProfileRegistry>();

  private CacheConfiguration configuration = CacheConfiguration.Default;

  void IConfigurableCache.Configure(CacheConfiguration configuration)
  {
    this.configuration = configuration;
  }

  async Task ICache<TValue>.Create(
    string key,
    TValue value,
    CancellationToken cancellationToken)
  {
    foreach (var policy in configuration.Policies)
    {
      await policy.HandleCache(
        new CreateCachePolicyContext(
          services,
          this,
          key,
          value
        ), cancellationToken);
    }

    publisher.Publish(
      new CreateCacheEventArgs
      {
        CacheConfiguration = configuration,
        Operation = CacheOperation.Create,
        Key = key,
        Value = value
      });
  }

  async Task<TValue?> ICache<TValue>.Read(
    string key,
    CancellationToken cancellationToken)
  {
    var value = await Read(key, cancellationToken);
    if (value is null)
    {
      return default;
    }

    var result = JsonSerializer.Deserialize<TValue>(
      value,
      configuration.JsonSerializerOptions
    );

    return result;
  }

  async Task ICache<TValue>.Delete(
    string key,
    CancellationToken cancellationToken)
  {
    foreach (var policy in configuration.Policies)
    {
      await policy.HandleCache(
        new CachePolicyContext(
          services,
          this,
          CacheOperation.Delete,
          key
        ), cancellationToken);
    }

    publisher.Publish(
      new CacheEventArgs
      {
        CacheConfiguration = configuration,
        Operation = CacheOperation.Delete,
        Key = key
      });
  }

  async Task IPolicyCache.Create(
    string key,
    object value,
    CancellationToken cancellationToken)
  {
    var type = value.GetType();
    var typeConfiguration = registry.GetConfiguration(type);

    var json = JsonSerializer.Serialize(
      value,
      type,
      typeConfiguration.JsonSerializerOptions
    );

    await Create(typeConfiguration.Entry, key, json, cancellationToken);
  }

  async Task<object?> IPolicyCache.Read(
    Type type,
    string key,
    CancellationToken cancellationToken
  )
  {
    var json = await Read(key, cancellationToken);
    if (json is null)
    {
      return default;
    }

    var typeConfiguration = registry.GetConfiguration(type);

    return JsonSerializer.Deserialize(
      json,
      type,
      typeConfiguration.JsonSerializerOptions
    );
  }

  async Task IPolicyCache.Delete(
    string key,
    CancellationToken cancellationToken
  )
  {
    await Delete(key, cancellationToken);
  }

  protected abstract Task Create(
    CacheEntryConfiguration entryConfiguration,
    string key,
    string value,
    CancellationToken cancellationToken);

  protected abstract Task<string?> Read(
    string key,
    CancellationToken cancellationToken);

  protected abstract Task<string?> Delete(
    string key,
    CancellationToken cancellationToken);
}
