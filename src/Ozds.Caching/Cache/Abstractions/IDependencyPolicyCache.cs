using Ozds.Caching.Entities.Dependencies;

namespace Ozds.Caching.Cache.Abstractions;

public interface IDependencyPolicyCache : IPolicyCache
{
  public Task CreateDependencies(
    string key,
    DependenciesEntity value,
    CancellationToken cancellationToken
  );

  public Task CreateReverseDependencies(
    string key,
    ReverseDependenciesEntity value,
    CancellationToken cancellationToken
  );

  public Task<DependenciesEntity?> ReadDependencies(
    string key,
    CancellationToken cancellationToken
  );

  public Task<ReverseDependenciesEntity?> ReadReverseDependencies(
    string key,
    CancellationToken cancellationToken
  );

  public Task DeleteDependencies(
    string key,
    CancellationToken cancellationToken
  );

  public Task DeleteReverseDependencies(
    string key,
    CancellationToken cancellationToken
  );

  public Task AddToReverseDependencies(
    string dependencyKey,
    string reverseDependencyKey,
    CancellationToken cancellationToken
  );

  public Task RemoveFromReverseDependencies(
    string dependencyKey,
    string reverseDependencyKey,
    CancellationToken cancellationToken
  );
}
