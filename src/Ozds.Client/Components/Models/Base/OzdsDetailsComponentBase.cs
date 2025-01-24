namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsDetailsComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind =>
    ModelComponentKind.Details;
}
