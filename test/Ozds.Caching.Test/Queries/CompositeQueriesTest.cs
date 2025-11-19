using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Queries;

public class CompositeEntityQueriesTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(CompositeEntities))]
  public async Task Read_EventuallyConsistent_WhenCompositeEntityIsCreated(
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
}
