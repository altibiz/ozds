using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Queries.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Queries;

public class EntityQueries(
  ICacheFactory cacheFactory,
  EntityReflector entityReflector
) : IQueries
{
  public async Task<T?> Read<T>(
    string id,
    CancellationToken cancellationToken
  )
    where T : notnull, IEntity
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

  public async Task<IEntity?> Read(
    Type entityType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(IEntity)))
    {
      throw new InvalidOperationException(
        "Entity type must implement IEntity");
    }

    foreach (var subtype in entityReflector.ResolveSubtypes(entityType))
    {
      var cache = cacheFactory.Create(subtype);
      var key = entityReflector.ResolveEntityKeyFromId(subtype, id);

      if (await cache.ReadObject(key, cancellationToken) is { } result)
      {
        return (IEntity?)result;
      }
    }

    return default;
  }
}
