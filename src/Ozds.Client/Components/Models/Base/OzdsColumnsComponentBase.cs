namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsColumnsComponentBase<TPrefix, TModel> :
  OzdsListModelComponentBase<TPrefix, TModel>
{
  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Columns; }
  }
}
