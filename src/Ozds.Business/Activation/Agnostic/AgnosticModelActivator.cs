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
    var activator = serviceProvider
        .GetServices<IModelActivator>()
        .FirstOrDefault(
          activator => activator.ModelType == modelType) ??
      throw new InvalidOperationException(
        $"No model activator found for {modelType}");
    return activator.Activate();
  }
}
