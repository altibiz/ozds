using Ozds.Caching.Cache;
using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Policies.Base;

namespace Ozds.Caching.Policies;

public class ReverseDependencyEvictionPolicy : DependencyPolicy
{
  public override async Task HandleCacheEvent(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    await base.HandleCacheEvent(policyContext, cancellationToken);

    var eventArgs = policyContext.EventArgs;
    if (
      eventArgs.Operation == CacheOperation.Delete
      || eventArgs.Operation == CacheOperation.Create
    )
    {
      await EvictReverseDependencies(policyContext, cancellationToken);
    }
  }
}
