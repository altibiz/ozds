using Ozds.Caching.Cache;
using Ozds.Caching.Cache.Dependencies;
using Ozds.Caching.Entities.Dependencies;
using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Policies.Base;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Policies;

public delegate string? IndirectReverseDependencyEvictionPolicyKeyResolver(
  object value);

public class IndirectReverseDependencyEvictionPolicy(
  IndirectReverseDependencyEvictionPolicyKeyResolver keyResolver,
  Type type
) : DependencyPolicy
{
  public override async Task HandleCacheEvent(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    var value = await Read(policyContext, cancellationToken);
    if (value is null)
    {
      // NOTE: nothing to delete/create
      return;
    }

    await base.HandleCacheEvent(policyContext, cancellationToken);

    var eventArgs = policyContext.EventArgs;
    if (eventArgs.Operation == CacheOperation.Delete
      || eventArgs.Operation == CacheOperation.Create)
    {
      var reverseDependencies = await ResolveReverseDependencies(
        policyContext,
        value,
        cancellationToken);
      if (reverseDependencies.ReverseDependencies.Count == 0)
      {
        return;
      }
      await EvictReverseDependencies(
        policyContext,
        cancellationToken,
        reverseDependencies);
    }
  }

  private async Task<ReverseDependenciesEntity> ResolveReverseDependencies(
    CacheEventPolicyContext policyContext,
    object value,
    CancellationToken cancellationToken
  )
  {
    var cache = new DependencyPolicyCache(policyContext.Cache);

    var entityReflector = policyContext.ServiceProvider
      .GetRequiredService<EntityReflector>();

    var reverseDependencies = new List<string>();
    foreach (var subtype in entityReflector
      .ResolveSubtypes(type))
    {
      var id = keyResolver(value);
      if (id is null)
      {
        continue;
      }

      var key = entityReflector
        .ResolveEntityKeyFromId(subtype, id);

      var scopeReverseDependencies = await cache
        .ReadReverseDependencies(
          key,
          cancellationToken);
      if (scopeReverseDependencies is null)
      {
        continue;
      }

      reverseDependencies.AddRange(
        scopeReverseDependencies.ReverseDependencies);
    }

    return new()
    {
      ReverseDependencies = reverseDependencies,
    };
  }
}
