using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Base;
using Ozds.Caching.Test.Extensions;

namespace Ozds.Caching.Test.Consistency;

public class CompositeConsistencyTest : OzdsCachingTestBase
{
  [Test]
  [MethodDataSource(nameof(CompositeEntities))]
  public async Task Crate_Delete_Create(
    Type type,
    CancellationToken cancellationToken
  )
  {
    var entity = EntityFactory.Create<ICompositeEntity>(type);
    var updated = EntityFactory.Create<ICompositeEntity>(type);
    updated.Id = entity.Id;

    await CompositeMutations.Create(entity, cancellationToken);
    await CompositeMutations.Delete(entity, cancellationToken);
    await CompositeMutations.Create(updated, cancellationToken);

    await Wait(cancellationToken);

    var result = await WaitFor<ICompositeEntity>(
      (r, _) => r is null || r.Title != updated.Title,
      type,
      entity.Id,
      cancellationToken
    );
    result.Should().BeRuntimeEquivalentTo(updated);
  }

  [Test]
  [MethodDataSource(nameof(CompositeEntitiesWithDependency))]
  public async Task Create_CreateDep_DeleteDep(
    CompositeEntityWithDependency entity,
    CancellationToken cancellationToken
  )
  {
    var composite = EntityFactory
      .Create<ICompositeEntity>(entity.Composite);
    var dependency = EntityFactory
      .Create<IIdentifiableEntity>(entity.Dependency);
    entity.SetId(composite, dependency);

    await CompositeMutations.Create(composite, cancellationToken);
    await IdentifiableMutations.Create(dependency, cancellationToken);
    await IdentifiableMutations.Delete(dependency, cancellationToken);

    await Wait(cancellationToken);

    var dependencyResult = await WaitFor<IIdentifiableEntity>(
      Null,
      entity.Dependency,
      dependency.Id,
      cancellationToken
    );
    dependencyResult.Should().BeNull();

    var compositeResult = await WaitFor<ICompositeEntity>(
      Null,
      entity.Composite,
      composite.Id,
      cancellationToken
    );
    compositeResult.Should().BeNull();
  }

  [Test]
  [MethodDataSource(nameof(CompositeEntitiesWithDependency))]
  public async Task Create_CreateDep_DeleteDep_CreateDep(
    CompositeEntityWithDependency entity,
    CancellationToken cancellationToken
  )
  {
    var composite = EntityFactory.Create<ICompositeEntity>(entity.Composite);
    var original = entity.Get(composite);
    var @new = EntityFactory.Create<IIdentifiableEntity>(entity.Dependency);
    @new.Id = original.Id;

    await CompositeMutations.Create(composite, cancellationToken);
    await IdentifiableMutations.Create(original, cancellationToken);
    await IdentifiableMutations.Delete(original, cancellationToken);
    await IdentifiableMutations.Create(@new, cancellationToken);

    await Wait(cancellationToken);

    var compositeResult = await WaitFor<ICompositeEntity>(
      Null,
      entity.Composite,
      composite.Id,
      cancellationToken);
    compositeResult.Should().BeNull();

    var dependencyResult = await WaitFor<IIdentifiableEntity>(
      NotNull,
      entity.Dependency,
      @new.Id,
      cancellationToken
    );
    dependencyResult.Should().BeRuntimeEquivalentTo(@new);
  }

  [Test]
  [MethodDataSource(nameof(CompositeEntitiesWithDependency))]
  public async Task Create_CreateDep_DeleteDep_DeleteDep(
    CompositeEntityWithDependency entity,
    CancellationToken cancellationToken
  )
  {
    var composite = EntityFactory.Create<ICompositeEntity>(entity.Composite);
    var dependency = entity.Get(composite);

    await CompositeMutations.Create(composite, cancellationToken);
    await IdentifiableMutations.Create(dependency, cancellationToken);
    await IdentifiableMutations.Delete(dependency, cancellationToken);
    await IdentifiableMutations.Delete(dependency, cancellationToken);

    await Wait(cancellationToken);

    var compositeResult = await WaitFor<ICompositeEntity>(
      Null,
      entity.Composite,
      composite.Id,
      cancellationToken
    );
    compositeResult.Should().BeNull();

    var dependencyResult = await WaitFor<IIdentifiableEntity>(
      Null,
      entity.Dependency,
      dependency.Id,
      cancellationToken
    );
    dependencyResult.Should().BeNull();
  }

  [Test]
  [MethodDataSource(nameof(CompositeEntitiesWithIndirectDependency))]
  public async Task Create_CreateInd(
    CompositeEntityWithIndirectDependency entity,
    CancellationToken cancellationToken
  )
  {
    var composite = EntityFactory.Create<ICompositeEntity>(entity.Composite);
    var indirect = EntityFactory.Create<IEntity>(entity.IndirectDependency);
    entity.SetId(composite, indirect);

    await CompositeMutations.Create(composite, cancellationToken);
    await EntityMutations.Create(
      indirect,
      entity.Key(indirect),
      cancellationToken
    );

    await Wait(cancellationToken);

    var compositeResult = await WaitFor<ICompositeEntity>(
      Null,
      entity.Composite,
      composite.Id,
      cancellationToken
    );
    compositeResult.Should().BeNull();

    var dependencyResult = await WaitFor<IEntity>(
      Null,
      entity.IndirectDependency,
      entity.Key(indirect),
      cancellationToken
    );
    dependencyResult.Should().BeRuntimeEquivalentTo(indirect);
  }
}
