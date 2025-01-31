using Ozds.Business.Activation.Abstractions;

namespace Ozds.Business.Activation.Base;

public abstract class InitializingModelActivator : IModelActivator
{
  public abstract Type ModelType { get; }

  public abstract bool CanActivate(Type type);

  public virtual object Activate()
  {
    var model = Box();
    Initialize(model);
    return model;
  }

  public abstract object Box();

  public virtual void Initialize(object model)
  {
  }
}
