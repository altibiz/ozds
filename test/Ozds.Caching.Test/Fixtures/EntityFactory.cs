using System.Reflection;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Test.Specimens;

namespace Ozds.Caching.Test.Fixtures;

public class EntityFactory
{
  private readonly Lazy<Fixture> fixture = new(() =>
  {
    var abstractEntities = EntitiesLike(entity => entity.IsAbstract);
    var concreteEntities = abstractEntities.ToDictionary(
      type => type,
      type => EntitiesLike(entity =>
        !entity.IsAbstract
        && entity.IsAssignableTo(type)));
    var created = new Fixture();
    foreach (var (type, concrete) in concreteEntities)
    {
      if (concrete.FirstOrDefault() is { } first)
      {
        created.Customize(new TypeRelay(type, first).ToCustomization());
      }
    }
    created.Customize(new ApiKeyAuthEntityBuilder().ToCustomization());
    return created;
  });

  private readonly Lazy<MethodInfo> genericCreateMethod = new(() =>
    typeof(SpecimenFactory)
      .GetMethods(BindingFlags.Static | BindingFlags.Public)
      .First(method =>
        method.Name == "Create"
        && method.IsGenericMethod
        && method.GetGenericArguments().Length == 1
        && method.GetParameters().Length == 1
        && method.GetParameters()[0].ParameterType == typeof(ISpecimenBuilder)));

  private Fixture Fixture => fixture.Value;

  private MethodInfo GenericCreateMethod => genericCreateMethod.Value;

  public TEntity Create<TEntity>()
    where TEntity : IEntity
  {
    return Fixture.Create<TEntity>();
  }

  public TEntity Create<TEntity>(Type entityType)
  {
    var createMethod = GenericCreateMethod.MakeGenericMethod(entityType);
    return (TEntity?)createMethod.Invoke(null, [Fixture])
      ?? throw new InvalidOperationException("Entity not created");
  }

  public IEnumerable<Type> CompositeEntities()
  {
    return EntitiesLike(type =>
      !type.IsAbstract
      && type.IsAssignableTo(typeof(ICompositeEntity)));
  }

  public IEnumerable<Type> IdentifiableEntities()
  {
    return EntitiesLike(type =>
      !type.IsAbstract
      && type.IsAssignableTo(typeof(IIdentifiableEntity)));
  }

  public IEnumerable<Type> JoinEntities()
  {
    return EntitiesLike(type =>
      !type.IsAbstract
      && type.IsAssignableTo(typeof(IJoinEntity)));
  }

  public IEnumerable<Type> Entities()
  {
    return EntitiesLike(type =>
      !type.IsAbstract
      && type.IsAssignableTo(typeof(IEntity))
      && !type.IsAssignableTo(typeof(IJoinEntity))
      && !type.IsAssignableTo(typeof(IIdentifiableEntity))
      && !type.IsAssignableTo(typeof(ICompositeEntity)));
  }

  public static IEnumerable<Type> EntitiesLike(Func<Type, bool> predicate)
  {
    return typeof(IEntity).Assembly.GetTypes()
      .Where(type =>
        type.Namespace is not null
        && type.Namespace.StartsWith("Ozds.Caching.Entities")
        && type.IsAssignableTo(typeof(IEntity))
        && predicate(type));
  }
}
