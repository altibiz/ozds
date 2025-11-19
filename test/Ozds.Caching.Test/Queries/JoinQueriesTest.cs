using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Queries;

// TODO: distinguish between slow/fast ones

public class JoinEntityQueriesTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(JoinEntities))]
  public async Task Read_EventuallyConsistent_WhenJoinEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IJoinEntity>(type);

    await JoinMutations.Create(entity, cancellationToken);

    var result = await WaitFor<IJoinEntity>(
      NotNull,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(entity);
  }
}
