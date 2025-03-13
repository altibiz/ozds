namespace Ozds.Client.Components.Models;
using Ozds.Client.Components.Models.Base;

public class PrefixedEdit<TPrefix, TModel>
  : ManagedModelComponent<TPrefix, TModel>
{
  protected override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Edit; }
  }
}
