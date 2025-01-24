using Ozds.Client.Components.Models.Base;

namespace Ozds.Client.Components.Models;

public partial class PrefixedColumns<TPrefix, TModel>
  : ListModelComponent<TPrefix, TModel>
{
  protected override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
