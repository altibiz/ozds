using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Queries;

public class IdentifiableEntityQueriesTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(IdentifiableEntities))]
  public async Task Read_EventuallyConsistent_WhenIdentifiableEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IIdentifiableEntity>(type);

    await IdentifiableMutations.Create(entity, cancellationToken);

    var result = await WaitFor<IIdentifiableEntity>(
      NotNull,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(entity);
  }
}
