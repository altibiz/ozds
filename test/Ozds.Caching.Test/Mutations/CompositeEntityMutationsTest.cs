using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Mutations;

public class CompositeEntityMutationsTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(CompositeEntities))]
  public async Task Create_EventuallyConsistent_WhenCompositeEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<ICompositeEntity>(type);

    await CompositeMutations.Create(entity, cancellationToken);

    var result = await WaitFor<ICompositeEntity>(
      NotNull,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(entity);
  }

  [Test]
  [MethodDataSource(nameof(CompositeEntities))]
  public async Task Delete_EventuallyConsistent_WhenCompositeEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<ICompositeEntity>(type);

    await CompositeMutations.Create(entity, cancellationToken);

    var result = await WaitFor<ICompositeEntity>(
      NotNull,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(entity);

    await CompositeMutations.Delete(entity, cancellationToken);

    result = await WaitFor<ICompositeEntity>(
      Null,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeNull();
  }
}
