using Ozds.Client.Components.Models.Abstractions;
using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class Details : ManagedModelComponent
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Details;
}
