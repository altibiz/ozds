using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Queries;

public class EntityQueriesTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(Entities))]
  public async Task Read_ImmediatelyConsistent_WhenEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IEntity>(type);

    await EntityMutations.Create(entity, "id", cancellationToken);

    var result = await EntityQueries.Read<IEntity>("id", cancellationToken);
    result.Should().BeRuntimeEquivalentTo(entity);
  }
}
