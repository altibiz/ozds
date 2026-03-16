using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Consistency;

[Repeat(10)]
public class IdentifiableConsistencyTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(IdentifiableEntities))]
  public async Task DeleteCreate_EventuallyConsistent_WhenIdentifiableEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IIdentifiableEntity>(type);
    var updated = EntityFactory.Create<IIdentifiableEntity>(type);
    updated.Id = entity.Id;

    await IdentifiableMutations.Create(entity, cancellationToken);
    await IdentifiableMutations.Delete(entity, cancellationToken);
    await IdentifiableMutations.Create(updated, cancellationToken);

    await Wait(cancellationToken);

    var result = await WaitFor<IIdentifiableEntity>(
      (result, _) => result is null || result.Title != updated.Title,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(updated);
  }

  [Test]
  [MethodDataSource(nameof(IdentifiableEntities))]
  public async Task Create_EventuallyConsistent_WhenIdentifiableEntityIsCreated(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<IIdentifiableEntity>(type);
    var updated = EntityFactory.Create<IIdentifiableEntity>(type);
    updated.Id = entity.Id;

    await IdentifiableMutations.Create(entity, cancellationToken);
    await IdentifiableMutations.Create(updated, cancellationToken);

    await Wait(cancellationToken);

    var result = await WaitFor<IIdentifiableEntity>(
      (result, _) => result is null || result.Title != updated.Title,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(updated);
  }
}
