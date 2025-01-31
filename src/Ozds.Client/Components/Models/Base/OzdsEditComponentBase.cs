namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsEditComponentBase<TPrefix, TModel> :
  OzdsManagedModelComponentBase<TPrefix, TModel>
{
  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Edit; }
  }
}
