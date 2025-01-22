namespace Ozds.Client.Components.Models.Abstractions;

public interface IModelComponentProvider
{
  public Type ModelType { get; }

  public bool CanRender(Type type);

  public Type ComponentType { get; }

  public ModelComponentKind ComponentKind { get; }
}
