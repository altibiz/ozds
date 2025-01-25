using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class PrefixedEdit<TPrefix, TModel>
  : ManagedModelComponent<TPrefix, TModel>
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Edit;
}
