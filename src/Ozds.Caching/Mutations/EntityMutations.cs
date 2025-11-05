using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Mutations.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Mutations;

public class EntityMutations(
  ICacheFactory cacheFactory,
  EntityReflector entityReflector
) : IMutations
{
  public async Task Create(
    IEntity entity,
    string id,
    CancellationToken cancellationToken
  )
  {
    var cache = cacheFactory.Create(entity.GetType());
    var key = entityReflector.ResolveEntityKeyFromId(entity.GetType(), id);

    await cache.CreateObject(key, entity, cancellationToken);
  }

  public async Task Delete(
    IEntity entity,
    string id,
    CancellationToken cancellationToken
  )
  {
    var cache = cacheFactory.Create(entity.GetType());
    var key = entityReflector.ResolveEntityKeyFromId(entity.GetType(), id);

    await cache.DeleteObject(key, cancellationToken);
  }
}
