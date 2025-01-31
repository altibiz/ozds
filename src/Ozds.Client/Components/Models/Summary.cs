using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class Summary : ManagedModelComponent
{
  protected override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Summary; }
  }
}
