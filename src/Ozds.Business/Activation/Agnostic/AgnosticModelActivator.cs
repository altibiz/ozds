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
    var activator = modelActivators.GetOrAdd(
      modelType,
      type => serviceProvider
          .GetServices<IModelActivator>()
          .FirstOrDefault(
            activator => activator.ModelType == type) ??
        throw new InvalidOperationException(
          $"No model activator found for {type}"));
    return activator.Activate();
  }

  private readonly ConcurrentDictionary<Type, IModelActivator> modelActivators =
    new();
}
