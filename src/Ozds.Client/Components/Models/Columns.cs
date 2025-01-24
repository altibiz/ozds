using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class Columns : ListModelComponent<object, object>
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
