using System.Collections.Concurrent;
using Ozds.Fake.Faking.Abstractions;

namespace Ozds.Fake.Faking;

public class ModelFaker(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<Type, IModelFaker> fakerCache = new();

  private readonly ConcurrentDictionary<Type, List<Type>> subtypeCache = new();

  public T Fake<T>()
    where T : notnull
  {
    return (T)FakeDynamic(typeof(T));
  }

  public object FakeDynamic(Type type)
  {
    if (fakerCache.TryGetValue(type, out var activator))
    {
      return activator.Fake();
    }

    activator =
      serviceProvider
        .GetServices<IModelFaker>()
        .Where(converter => converter.CanFake(type))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null ? null
            : next!.ModelType.IsAssignableTo(acc.ModelType) ? next
            : acc
        )
      ?? throw new InvalidOperationException(
        $"No model activator found for {type}"
      );

    fakerCache.TryAdd(type, activator);

    return activator.Fake();
  }

  public List<Type> FakeableSubtypes(Type type)
  {
    if (subtypeCache.TryGetValue(type, out var subtypes))
    {
      return subtypes;
    }

    subtypes = serviceProvider
      .GetServices<IModelFaker>()
      .Where(converter =>
        !converter.ModelType.IsAbstract
        && !converter.ModelType.IsInterface
        && converter.ModelType.IsAssignableTo(type)
        && converter.CanFake(type)
      )
      .Select(converter => converter.ModelType)
      .ToList();

    subtypes = subtypes
      .Where(subtype => !subtypes.Exists(type => type.BaseType == subtype))
      .ToList();

    subtypeCache.TryAdd(type, subtypes);

    return subtypes;
  }
}
