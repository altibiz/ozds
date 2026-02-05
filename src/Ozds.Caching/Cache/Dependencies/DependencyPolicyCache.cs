using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Entities.Dependencies;

namespace Ozds.Caching.Cache.Dependencies;

public class DependencyPolicyCache(IPolicyCache policyCache)
  : IDependencyPolicyCache
{
  private const string DependenciesSuffix = "#dependencies";

  private const string ReverseDependenciesSuffix = "#reverse-dependencies";

  public Task Create(
    string key,
    object value,
    CancellationToken cancellationToken
  )
  {
    return policyCache.Create(key, value, cancellationToken);
  }

  public Task<object?> Read(
    Type type,
    string key,
    CancellationToken cancellationToken
  )
  {
    return policyCache.Read(type, key, cancellationToken);
  }

  public Task Delete(string key, CancellationToken cancellationToken)
  {
    return policyCache.Delete(key, cancellationToken);
  }

  public Task CreateDependencies(
    string key,
    DependenciesEntity value,
    CancellationToken cancellationToken
  )
  {
    return policyCache.Create(
      GetDependenciesCacheKey(key),
      value,
      cancellationToken
    );
  }

  public Task CreateReverseDependencies(
    string key,
    ReverseDependenciesEntity value,
    CancellationToken cancellationToken
  )
  {
    return policyCache.Create(
      GetReverseDependenciesCacheKey(key),
      value,
      cancellationToken
    );
  }

  public async Task<DependenciesEntity?> ReadDependencies(
    string key,
    CancellationToken cancellationToken
  )
  {
    return await policyCache.Read(
        typeof(DependenciesEntity),
        GetDependenciesCacheKey(key),
        cancellationToken
      ) as DependenciesEntity;
  }

  public async Task<ReverseDependenciesEntity?> ReadReverseDependencies(
    string key,
    CancellationToken cancellationToken
  )
  {
    return await policyCache.Read(
        typeof(ReverseDependenciesEntity),
        GetReverseDependenciesCacheKey(key),
        cancellationToken
      ) as ReverseDependenciesEntity;
  }

  public Task DeleteDependencies(
    string key,
    CancellationToken cancellationToken
  )
  {
    return policyCache.Delete(GetDependenciesCacheKey(key), cancellationToken);
  }

  public Task DeleteReverseDependencies(
    string key,
    CancellationToken cancellationToken
  )
  {
    return policyCache.Delete(
      GetReverseDependenciesCacheKey(key),
      cancellationToken
    );
  }

  public async Task AddToReverseDependencies(
    string dependencyKey,
    string reverseDependencyKey,
    CancellationToken cancellationToken
  )
  {
    var reverseDependencies = await ReadReverseDependencies(
      dependencyKey,
      cancellationToken
    );

    if (
      reverseDependencies is null
      || !reverseDependencies.ReverseDependencies.Contains(reverseDependencyKey)
    )
    {
      reverseDependencies ??= new ReverseDependenciesEntity();
      reverseDependencies.ReverseDependencies.Add(reverseDependencyKey);
      await CreateReverseDependencies(
        dependencyKey,
        reverseDependencies,
        cancellationToken
      );
    }
  }

  public async Task RemoveFromReverseDependencies(
    string dependencyKey,
    string reverseDependencyKey,
    CancellationToken cancellationToken
  )
  {
    var reverseDependencies = await ReadReverseDependencies(
      dependencyKey,
      cancellationToken
    );

    if (
      reverseDependencies is not null
      && reverseDependencies.ReverseDependencies.Contains(reverseDependencyKey)
    )
    {
      reverseDependencies.ReverseDependencies.Remove(reverseDependencyKey);
      if (reverseDependencies.ReverseDependencies.Count == 0)
      {
        await DeleteReverseDependencies(dependencyKey, cancellationToken);
      }
      else
      {
        await CreateReverseDependencies(
          dependencyKey,
          reverseDependencies,
          cancellationToken
        );
      }
    }
  }

  private static string GetDependenciesCacheKey(string key)
  {
    return $"{key}{DependenciesSuffix}";
  }

  private static string GetReverseDependenciesCacheKey(string key)
  {
    return $"{key}{ReverseDependenciesSuffix}";
  }
}
