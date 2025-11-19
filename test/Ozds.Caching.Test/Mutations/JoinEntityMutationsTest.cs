using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Mutations;

public class JoinEntityMutationsTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(JoinEntities))]
  public async Task Create_EventuallyConsistent_WhenJoinEntityIsCreated(
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

  [Test]
  [MethodDataSource(nameof(JoinEntities))]
  public async Task Delete_EventuallyConsistent_WhenJoinEntityIsCreated(
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

    await JoinMutations.Delete(entity, cancellationToken);

    result = await WaitFor<IJoinEntity>(
      Null,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeNull();
  }
}
