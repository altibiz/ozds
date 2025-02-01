using System.Collections.Concurrent;
using Ozds.Business.Activation.Abstractions;

namespace Ozds.Business.Activation;

public class ModelActivator(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<Type, IModelActivator> activatorCache = new();

  private readonly ConcurrentDictionary<Type, List<Type>> subtypeCache = new();

  public T Activate<T>()
    where T : notnull
  {
    return (T)ActivateDynamic(typeof(T));
  }

  public object ActivateDynamic(Type type)
  {
    if (activatorCache.TryGetValue(type, out var activator))
    {
      return activator.Activate();
    }

    activator = serviceProvider
        .GetServices<IModelActivator>()
        .Where(converter => converter.CanActivate(type))
        .DefaultIfEmpty(null)
        .Aggregate(
          (acc, next) =>
            acc is null
              ? null
              : next!.ModelType.IsAssignableTo(acc.ModelType)
                ? next
                : acc)
      ?? throw new InvalidOperationException(
        $"No model activator found for {type}");

    activatorCache.TryAdd(type, activator);

    return activator.Activate();
  }

  public List<Type> ActivatableSubtypes(Type type)
  {
    if (subtypeCache.TryGetValue(type, out var subtypes))
    {
      return subtypes;
    }

    subtypes = serviceProvider
      .GetServices<IModelActivator>()
      .Where(converter =>
        !converter.ModelType.IsAbstract
        && !converter.ModelType.IsInterface
        && converter.ModelType.IsAssignableTo(type)
        && converter.CanActivate(type))
      .Select(converter => converter.ModelType)
      .ToList();

    subtypes = subtypes
      .Where(subtype => !subtypes.Exists(type => type.BaseType == subtype))
      .ToList();

    subtypeCache.TryAdd(type, subtypes);

    return subtypes;
  }
}
