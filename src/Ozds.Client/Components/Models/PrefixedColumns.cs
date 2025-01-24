using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class PrefixedColumns<TModel, TPrefix>
  : ListModelComponent<TModel, TPrefix>
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
