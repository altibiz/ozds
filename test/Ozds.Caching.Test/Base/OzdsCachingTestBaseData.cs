using Ozds.Caching.Entities;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Composite;
using Ozds.Caching.Entities.Joins;

namespace Ozds.Caching.Test.Base;

public partial class OzdsCachingTestBase
{
  public IEnumerable<Type> Entities()
  {
    return EntityFactory.Entities();
  }

  public IEnumerable<Type> CompositeEntities()
  {
    return EntityFactory.CompositeEntities();
  }

  public IEnumerable<Type> IdentifiableEntities()
  {
    return EntityFactory.IdentifiableEntities();
  }

  public IEnumerable<Type> JoinEntities()
  {
    return EntityFactory.JoinEntities();
  }

  public IEnumerable<CompositeEntityWithDependency>
    CompositeEntitiesWithDependency()
  {
    yield return
      new CompositeEntityWithDependency<ApiKeyAuthEntity, ApiKeyEntity>(
      (composite, dependency) =>
      {
        dependency.Id = composite.ApiKey.Id;
      },
      composite => composite.ApiKey);
    yield return
      new CompositeEntityWithDependency<ApiKeyAuthEntity, ScopeEntity>(
      (composite, dependency) =>
      {
        dependency.Id =
          composite.Scopes.First(x => x is not MeasurementScopeEntity).Id;
      },
      composite =>
        composite.Scopes.First(x => x is not MeasurementScopeEntity));
    yield return
      new CompositeEntityWithDependency<ApiKeyAuthEntity, MeasurementScopeEntity>(
      (composite, dependency) =>
      {
        dependency.Id =
          composite.Scopes.First(x => x is MeasurementScopeEntity).Id;
      },
      composite =>
        (composite.Scopes
          .First(x => x is MeasurementScopeEntity)
          as MeasurementScopeEntity)!);
    yield return
      new CompositeEntityWithDependency<ApiKeyAuthEntity, RegisterEntity>(
      (composite, dependency) =>
      {
        dependency.Id = composite.Registers[0].Id;
      },
      composite => composite.Registers[0]);
    yield return
      new CompositeEntityWithDependency<
      MeterMeasurementLocationEntity,
      IMeterEntity>(
      (composite, dependency) =>
      {
        dependency.Id = composite.Meter.Id;
      },
      composite => composite.Meter);
    yield return
      new CompositeEntityWithDependency<
      MeterMeasurementLocationEntity,
      IMeasurementLocationEntity>(
      (composite, dependency) =>
      {
        dependency.Id = composite.MeasurementLocation.Id;
      },
      composite => composite.MeasurementLocation);
  }

  public record CompositeEntityWithDependency(
    Type Composite,
    Type Dependency,
    Action<ICompositeEntity, IIdentifiableEntity> SetId,
    Func<ICompositeEntity, IIdentifiableEntity> Get
  );

  public sealed record CompositeEntityWithDependency<TComposite, TDependency>(
    Action<TComposite, TDependency> SetIdTyped,
    Func<TComposite, TDependency> GetTyped
  ) : CompositeEntityWithDependency(
    typeof(TComposite),
    typeof(TDependency),
    (composite, dependency) =>
    {
      var compositeEntity = (TComposite)composite;
      var dependencyEntity = (TDependency)dependency;
      SetIdTyped(compositeEntity, dependencyEntity);
    },
    composite =>
    {
      var compositeEntity = (TComposite)composite;
      var dependencyEntity = GetTyped(compositeEntity);
      return dependencyEntity;
    }
  )
    where TComposite : ICompositeEntity
    where TDependency : IIdentifiableEntity;

  public IEnumerable<CompositeEntityWithIndirectDependency>
    CompositeEntitiesWithIndirectDependency()
  {
    yield return
      new CompositeEntityWithIndirectDependency<ApiKeyAuthEntity, ApiKeyScopeEntity>(
      (composite, dependency) =>
      {
        dependency.ApiKeyId = composite.ApiKey.Id;
      },
      dependency => (dependency as IJoinEntity).Id);
    yield return
      new CompositeEntityWithIndirectDependency<ApiKeyAuthEntity, ApiKeyScopeEntity>(
      (composite, dependency) =>
      {
        dependency.ScopeId = composite.Scopes[0].Id;
      },
      dependency => (dependency as IJoinEntity).Id);
    yield return
      new CompositeEntityWithIndirectDependency<ApiKeyAuthEntity, RegisterEntity>(
      (composite, dependency) =>
      {
        dependency.ScopeId = composite.Registers[0].ScopeId;
      },
      dependency => dependency.Id);
  }

  public record CompositeEntityWithIndirectDependency(
    Type Composite,
    Type IndirectDependency,
    Action<ICompositeEntity, object> SetId,
    Func<object, string> Key
  );

  public sealed record CompositeEntityWithIndirectDependency<TComposite, TIndirectDependency>(
    Action<TComposite, TIndirectDependency> SetIdTyped,
    Func<TIndirectDependency, string> KeyTyped
  ) : CompositeEntityWithIndirectDependency(
    typeof(TComposite),
    typeof(TIndirectDependency),
    (composite, dependency) =>
    {
      var compositeEntity = (TComposite)composite;
      var dependencyEntity = (TIndirectDependency)dependency;
      SetIdTyped(compositeEntity, dependencyEntity);
    },
    (dependency) =>
    {
      var dependencyEntity = (TIndirectDependency)dependency;
      return KeyTyped(dependencyEntity);
    }
  )
    where TComposite : ICompositeEntity;
}
