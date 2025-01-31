namespace Ozds.Client.Components.Models.Base;

public abstract partial class OzdsSummaryComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind
  {
    get { return ModelComponentKind.Summary; }
  }
}
