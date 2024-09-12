using Ozds.Business.Activation.Abstractions;

namespace Ozds.Business.Activation.Base;

public abstract class ModelActivator<T> : IModelActivator
  where T : notnull
{
  public Type ModelType
  {
    get { return typeof(T); }
  }

  public object Activate()
  {
    return ActivateConcrete();
  }

  public virtual T ActivateConcrete()
  {
    return Activator.CreateInstance<T>();
  }
}
