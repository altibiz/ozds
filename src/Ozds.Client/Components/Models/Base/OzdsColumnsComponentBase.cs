namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsColumnsComponentBase<TPrefix, TModel> :
  OzdsListModelComponentBase<TPrefix, TModel>
{
  protected virtual int NestingLevel => 1;

  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Columns; }
  }
}
