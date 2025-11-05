using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Queries.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Queries;

public class IdentifiableEntityQueries(
  ICacheFactory cacheFactory,
  EntityReflector entityReflector
) : IQueries
{
  public async Task<T?> Read<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : notnull, IIdentifiableEntity
  {
    foreach (var subtype in entityReflector.ResolveSubtypes<T>())
    {
      var cache = cacheFactory.Create(subtype);
      var key = entityReflector.ResolveEntityKeyFromId(subtype, id);
      if (await cache.ReadObject(key, cancellationToken) is { } result)
      {
        return (T?)result;
      }
    }

    return default;
  }

  public async Task<IIdentifiableEntity?> Read(
    Type entityType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IIdentifiableEntity)))
    {
      throw new InvalidOperationException(
        "Entity type must implement IIdentifiableEntity");
    }

    foreach (var subtype in entityReflector.ResolveSubtypes(entityType))
    {
      var cache = cacheFactory.Create(subtype);
      var key = entityReflector.ResolveEntityKeyFromId(subtype, id);
      if (await cache.ReadObject(key, cancellationToken) is { } result)
      {
        return (IIdentifiableEntity?)result;
      }
    }

    return default;
  }
}
