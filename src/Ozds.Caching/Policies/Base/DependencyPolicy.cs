using System.Runtime.CompilerServices;
using Ozds.Caching.Cache.Dependencies;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Dependencies;
using Ozds.Caching.Policies.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Policies.Base;

public abstract class DependencyPolicy : Policy
{
  public override Task HandleCache(
    CachePolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    return Task.CompletedTask;
  }

  public override async Task HandleCacheEvent(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    if (policyContext
      is CreateCacheEventPolicyContext createCachePolicyContext)
    {
      await policyContext.Cache.Create(
        createCachePolicyContext.CreateEventArgs.Key,
        createCachePolicyContext.CreateEventArgs.Value,
        cancellationToken);
    }
    else
    {
      await policyContext.Cache.Delete(
        policyContext.EventArgs.Key,
        cancellationToken);
    }
  }

  protected async Task SetDependencies(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken
  )
  {
    var cacheKey = policyContext.EventArgs.Key;

    var dependencies = await
      GetDependencies(policyContext, cancellationToken)
        .ToListAsync(cancellationToken);
    if (dependencies.Count == 0)
    {
      return;
    }

    var entityReflector = policyContext.ServiceProvider
      .GetRequiredService<EntityReflector>();
    var cache = new DependencyPolicyCache(policyContext.Cache);

    var dependencyEntity = new DependenciesEntity();

    foreach (var dependency in dependencies)
    {
      var dependencyKey = entityReflector
        .ResolveEntityKeyFromIdentifiable(dependency);

      dependencyEntity.Dependencies.Add(dependencyKey);

      await cache.Create(dependencyKey, dependency, cancellationToken);

      await cache.AddToReverseDependencies(
        dependencyKey,
        cacheKey,
        cancellationToken
      );
    }

    await cache.CreateDependencies(
      cacheKey,
      dependencyEntity,
      cancellationToken);
  }

  protected async Task EvictDependencyReferences(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken,
    string? cacheKey = null,
    object? value = null
  )
  {
    cacheKey ??= policyContext.EventArgs.Key;

    var dependencies = await
      GetDependencyReferences(policyContext, cancellationToken, value)
        .ToListAsync(cancellationToken);
    if (dependencies.Count == 0)
    {
      return;
    }

    var cache = new DependencyPolicyCache(policyContext.Cache);

    await cache.DeleteDependencies(
      cacheKey,
      cancellationToken);

    foreach (var dependency in dependencies)
    {
      await cache.RemoveFromReverseDependencies(
        dependency,
        cacheKey,
        cancellationToken
      );
    }
  }

  protected async Task EvictReverseDependencies(
    CacheEventPolicyContext policyContext,
    CancellationToken cancellationToken,
    ReverseDependenciesEntity? reverseDependencies = null
  )
  {
    var cache = new DependencyPolicyCache(policyContext.Cache);

    var cacheKey = policyContext.EventArgs.Key;
    reverseDependencies ??= await cache
      .ReadReverseDependencies(
        cacheKey,
        cancellationToken);
    if (reverseDependencies is null)
    {
      return;
    }

    var reflector = policyContext.ServiceProvider
      .GetRequiredService<EntityReflector>();

    foreach (var reverseDependency in reverseDependencies.ReverseDependencies)
    {
      var value = await cache.Read(
        reflector.ResolveEntityTypeFromKey(reverseDependency),
        reverseDependency,
        cancellationToken
      );
      await EvictDependencyReferences(
        policyContext,
        cancellationToken,
        reverseDependency,
        value
      );
      await cache.Delete(
        reverseDependency,
        cancellationToken
      );
    }
  }

  protected IAsyncEnumerable<string> GetDependencyReferences(
    PolicyContext policyContext,
    CancellationToken cancellationToken,
    object? value = null
  )
  {
    var entityReflector = policyContext.ServiceProvider
      .GetRequiredService<EntityReflector>();

    return GetDependencies(policyContext, cancellationToken, value)
      .Select(entityReflector.ResolveEntityKeyFromIdentifiable);
  }

  protected async IAsyncEnumerable<IIdentifiableEntity> GetDependencies(
    PolicyContext policyContext,
    [EnumeratorCancellation] CancellationToken cancellationToken,
    object? value = null
  )
  {
    value ??= await Read(policyContext, cancellationToken);
    if (value == null)
    {
      yield break;
    }

    foreach (var property in value
      .GetType()
      .GetProperties()
      .Where(property => property.CanRead)
      .Where(
        property =>
          property.PropertyType.IsAssignableTo(typeof(IIdentifiableEntity))
          || property.PropertyType
            .IsAssignableTo(typeof(IEnumerable<IIdentifiableEntity>))))
    {
      var dependencyValue = property.GetValue(value);
      if (dependencyValue is IIdentifiableEntity identifiable)
      {
        await foreach (var dependency in
          GetDependencies(
            policyContext,
            cancellationToken,
            identifiable))
        {
          yield return dependency;
        }

        yield return identifiable;
      }
      else if (dependencyValue
        is IEnumerable<IIdentifiableEntity> dependencyIdentifiables)
      {
        foreach (var dependencyIdentifiable in dependencyIdentifiables)
        {
          await foreach (var dependency in
            GetDependencies(
              policyContext,
              cancellationToken,
              dependencyIdentifiable))
          {
            yield return dependency;
          }

          yield return dependencyIdentifiable;
        }
      }
    }
  }
}
