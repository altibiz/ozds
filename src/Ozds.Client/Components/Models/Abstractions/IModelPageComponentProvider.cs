namespace Ozds.Client.Components.Models.Abstractions;

public interface IModelPageComponentProvider
{
  public Type ModelType { get; }

  public bool CanRender(Type modelType);

  public Type PageType { get; }

  public string CreateLink(object model);
}
