using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsColumnsComponentBase<TModel> :
  OzdsListModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
