using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsColumnsComponentBase<TModel, TConstraintType> :
  OzdsListModelComponentBase<TModel, TConstraintType>
  where TModel : TConstraintType
{
  public override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
