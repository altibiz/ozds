using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class Edit<TPrefix> : ManagedModelComponent<TPrefix, TPrefix>
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Edit;
}
