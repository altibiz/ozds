using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Mutations;

public class IdentifiableEntityMutationsTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(IdentifiableEntities))]
  public async Task Create_EventuallyConsistent_WhenIdentifiableEntityIsCreated(
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

  [Test]
  [MethodDataSource(nameof(IdentifiableEntities))]
  public async Task Delete_EventuallyConsistent_WhenIdentifiableEntityIsCreated(
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

    await IdentifiableMutations.Delete(entity, cancellationToken);

    result = await WaitFor<IIdentifiableEntity>(
      Null,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeNull();
  }
}
