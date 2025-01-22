using Ozds.Client.Components.Models.Abstractions;
using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class Columns : ListModelComponent
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
