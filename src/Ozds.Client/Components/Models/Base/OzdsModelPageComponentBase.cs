using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsModelPageComponentBase<T>
  : OzdsComponentBase, IModelPageComponentProvider
{
  protected abstract string CreateLink(
    NavigationManager navigationManager,
    TemplateBinderFactory factory,
    T model
  );

  public Type ModelType => typeof(T);

  public Type PageType => GetType();

  public bool CanRender(Type modelType)
  {
    return modelType.IsAssignableTo(ModelType);
  }

  public string CreateLink(
    NavigationManager navigationManager,
    TemplateBinderFactory factory,
    object model
  )
  {
    return CreateLink(navigationManager, factory, (T)model);
  }
}
