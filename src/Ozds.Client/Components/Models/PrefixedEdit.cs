using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class PrefixedEdit<TPrefix, TModel>
  : ManagedModelComponent<TPrefix, TModel>
{
  protected override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Edit; }
  }
}
