using System.Collections.Concurrent;
using Ozds.Business.Activation.Abstractions;

namespace Ozds.Business.Activation.Agnostic;

public class AgnosticModelActivator(IServiceProvider serviceProvider)
{
  public T Activate<T>()
    where T : notnull
  {
    return (T)ActivateDynamic(typeof(T));
  }

  public object ActivateDynamic(Type modelType)
  {
    if (modelActivators.TryGetValue(modelType, out var activator))
    {
      return activator.Activate();
    }

    activator = serviceProvider
      .GetServices<IModelActivator>()
      .FirstOrDefault(
        activator => activator.ModelType == modelType)
      ?? throw new InvalidOperationException(
          $"No model activator found for {modelType}");

    modelActivators.TryAdd(modelType, activator);

    return activator.Activate();
  }

  private readonly ConcurrentDictionary<Type, IModelActivator> modelActivators =
    new();
}
