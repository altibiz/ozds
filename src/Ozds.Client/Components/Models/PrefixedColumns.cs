using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public class PrefixedColumns<TPrefix, TModel>
  : ListModelComponent<TPrefix, TModel>
{
  protected override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Columns; }
  }
}
