using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;

namespace Ozds.Client.Components.Models.Abstractions;

public interface IModelPageComponentProvider
{
  public Type ModelType { get; }

  public Type PageType { get; }

  public bool CanRender(Type modelType);

  public string CreateLink(
    NavigationManager navigationManager,
    TemplateBinderFactory factory,
    object model
  );
}
