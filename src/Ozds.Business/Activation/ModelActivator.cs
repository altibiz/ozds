using System.Collections.Concurrent;
using Ozds.Business.Activation.Abstractions;

namespace Ozds.Business.Activation;

public class ModelActivator(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<Type, IModelActivator> cache = new();

  public T Activate<T>()
    where T : notnull
  {
    return (T)ActivateDynamic(typeof(T));
  }

  public object ActivateDynamic(Type type)
  {
    if (cache.TryGetValue(type, out var activator))
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

    cache.TryAdd(type, activator);

    return activator.Activate();
  }
}
