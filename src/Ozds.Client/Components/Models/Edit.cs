using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class Edit : ManagedModelComponent
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Edit;
}
