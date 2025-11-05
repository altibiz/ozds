using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Mutations;
using Ozds.Caching.Queries;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Mutations;

public class EntityMutationsTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(Entities))]
  public async Task Create_ImmediatelyConsistent_WhenEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IEntity>(type);
    await EntityMutations.Create(entity, "id", cancellationToken);

    var result = await EntityQueries.Read(type, "id", cancellationToken);
    result.Should().BeRuntimeEquivalentTo(entity);
  }

  [Test]
  [MethodDataSource(nameof(Entities))]
  public async Task Delete_ImmediatelyConsistent_WhenEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IEntity>(type);
    await EntityMutations.Create(entity, "id", cancellationToken);

    var result = await EntityQueries.Read(type, "id", cancellationToken);
    result.Should().BeRuntimeEquivalentTo(entity);

    await EntityMutations.Delete(entity, "id", cancellationToken);

    result = await EntityQueries.Read(type, "id", cancellationToken);
    result.Should().BeNull();
  }
}
