using Ozds.Caching.Cache;
using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Observers.EventArgs;

namespace Ozds.Caching.Policies.Abstractions;

public record PolicyContext(
  IServiceProvider ServiceProvider,
  IPolicyCache Cache
);

public record CachePolicyContext(
  IServiceProvider ServiceProvider,
  IPolicyCache Cache,
  CacheOperation Operation,
  string Key
) : PolicyContext(ServiceProvider, Cache);

public record CreateCachePolicyContext(
  IServiceProvider ServiceProvider,
  IPolicyCache Cache,
  string Key,
  object Value
) : CachePolicyContext(ServiceProvider, Cache, CacheOperation.Create, Key);

public record CacheEventPolicyContext(
  IServiceProvider ServiceProvider,
  IPolicyCache Cache,
  CacheEventArgs EventArgs
) : PolicyContext(ServiceProvider, Cache);

public record CreateCacheEventPolicyContext(
  IServiceProvider ServiceProvider,
  IPolicyCache Cache,
  CreateCacheEventArgs CreateEventArgs
) : CacheEventPolicyContext(ServiceProvider, Cache, CreateEventArgs);

public interface IPolicy
{
  public Task HandleCache(
    CachePolicyContext policyContext,
    CancellationToken cancellationToken
  );

  public Task HandleCacheEvent(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  );
}
