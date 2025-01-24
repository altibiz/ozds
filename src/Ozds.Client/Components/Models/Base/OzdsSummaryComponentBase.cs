namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsSummaryComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public override ModelComponentKind ComponentKind =>
    ModelComponentKind.Summary;
}
