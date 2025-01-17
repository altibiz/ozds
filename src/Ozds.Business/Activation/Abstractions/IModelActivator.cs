namespace Ozds.Business.Activation.Abstractions;

public interface IModelActivator
{
  public Type ModelType { get; }

  public bool CanActivate(Type type);

  public object Activate();
}
