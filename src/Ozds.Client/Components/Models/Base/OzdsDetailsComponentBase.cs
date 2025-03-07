namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsDetailsComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Details; }
  }
}
