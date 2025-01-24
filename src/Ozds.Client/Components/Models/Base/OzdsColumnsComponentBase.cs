namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsColumnsComponentBase<TPrefix, TModel> :
  OzdsListModelComponentBase<TPrefix, TModel>
{
  public override ModelComponentKind ComponentKind =>
    ModelComponentKind.Columns;
}
