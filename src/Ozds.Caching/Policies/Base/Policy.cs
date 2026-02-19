using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Policies.Base;

public abstract class Policy : IPolicy
{
  public abstract Task HandleCache(
    CachePolicyContext policyContext,
    CancellationToken cancellationToken
  );

  public abstract Task HandleCacheEvent(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  );

  protected async Task<object?> Read(
    PolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    if (policyContext is CreateCachePolicyContext createCachePolicyContext)
    {
      return createCachePolicyContext.Value;
    }

    if (
      policyContext
      is CreateCacheEventPolicyContext createCacheEventPolicyContext
    )
    {
      return createCacheEventPolicyContext.CreateEventArgs.Value;
    }

    var key = policyContext switch
    {
      CachePolicyContext cachePolicyContext => cachePolicyContext.Key,
      CacheEventPolicyContext cacheEventPolicyContext => cacheEventPolicyContext
        .EventArgs
        .Key,
      _ => throw new InvalidOperationException("Unknown policy context"),
    };

    var reflector =
      policyContext.ServiceProvider.GetRequiredService<EntityReflector>();
    var type = reflector.ResolveEntityTypeFromKey(key);

    var value = await policyContext.Cache.Read(type, key, cancellationToken);

    return value;
  }
}
