using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Queries.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Queries;

public class CompositeEntityQueries(
  ICacheFactory cacheFactory,
  EntityReflector entityReflector
) : IQueries
{
  public async Task<T?> Read<T>(string id, CancellationToken cancellationToken)
    where T : notnull, ICompositeEntity
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

  public async Task<ICompositeEntity?> Read(
    Type entityType,
    string id,
    CancellationToken cancellationToken
  )
  {
    if (!entityType.IsAssignableTo(typeof(ICompositeEntity)))
    {
      throw new InvalidOperationException(
        "Entity type must implement ICompositeEntity"
      );
    }

    foreach (var subtype in entityReflector.ResolveSubtypes(entityType))
    {
      var cache = cacheFactory.Create(subtype);
      var key = entityReflector.ResolveEntityKeyFromId(subtype, id);
      if (await cache.ReadObject(key, cancellationToken) is { } result)
      {
        return (ICompositeEntity?)result;
      }
    }

    return default;
  }
}
