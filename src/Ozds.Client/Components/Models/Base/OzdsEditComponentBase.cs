namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsEditComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind =>
    ModelComponentKind.Edit;
}
