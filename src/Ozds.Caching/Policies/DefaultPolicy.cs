using Ozds.Caching.Cache;
using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Policies.Base;

namespace Ozds.Caching.Policies;

public class DefaultPolicy : Policy
{
  public override async Task HandleCache(
    CachePolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    if (policyContext is CreateCachePolicyContext createPolicyContext)
    {
      await policyContext.Cache.Create(
        createPolicyContext.Key,
        createPolicyContext.Value,
        cancellationToken
      );
    }
    else if (policyContext.Operation == CacheOperation.Delete)
    {
      await policyContext.Cache.Delete(policyContext.Key, cancellationToken);
    }
  }

  public override Task HandleCacheEvent(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    return Task.CompletedTask;
  }
}
