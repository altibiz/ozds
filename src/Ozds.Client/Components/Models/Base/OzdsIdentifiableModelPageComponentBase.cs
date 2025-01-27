using Microsoft.AspNetCore.Routing.Template;
using Ozds.Business.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsIdentifiableModelPageComponentBase<T>
  : OzdsModelPageComponentBase<T>
  where T : IIdentifiable
{
  protected override string CreateLink(
    TemplateBinderFactory factory,
    T model
  )
  {
    return PageHref(PageType, new { model.Id });
  }
}
