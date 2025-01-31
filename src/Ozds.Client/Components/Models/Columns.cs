using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class Columns<TPrefix> : ListModelComponent<TPrefix, TPrefix>
{
  protected override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Columns; }
  }
}
