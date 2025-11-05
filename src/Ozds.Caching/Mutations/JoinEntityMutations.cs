using Ozds.Caching.Cache.Abstractions;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Mutations.Abstractions;
using Ozds.Caching.Reflection;

namespace Ozds.Caching.Mutations;

public class JoinEntityMutations(
  ICacheFactory cacheFactory,
  EntityReflector entityReflector
) : IMutations
{
  public async Task Create(
    IJoinEntity entity,
    CancellationToken cancellationToken
  )
  {
    var cache = cacheFactory.Create(entity.GetType());
    var key = entityReflector.ResolveEntityKeyFromJoin(entity);

    await cache.CreateObject(key, entity, cancellationToken);
  }

  public async Task Delete(
    IJoinEntity entity,
    CancellationToken cancellationToken
  )
  {
    var cache = cacheFactory.Create(entity.GetType());
    var key = entityReflector.ResolveEntityKeyFromJoin(entity);

    await cache.DeleteObject(key, cancellationToken);
  }
}
