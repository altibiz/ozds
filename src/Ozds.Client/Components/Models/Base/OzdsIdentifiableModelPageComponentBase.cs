using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;
using Ozds.Business.Models.Abstractions;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsIdentifiableModelPageComponentBase<T>
  : OzdsModelPageComponentBase<T>
  where T : IIdentifiable
{
  protected override string CreateLink(
    NavigationManager navigationManager,
    TemplateBinderFactory factory,
    T model
  )
  {
    return navigationManager.PageHref(factory, PageType, new { model.Id });
  }
}
