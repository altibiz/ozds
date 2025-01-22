using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsSummaryComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public class ModelComponentProvider : ModelComponentProviderBase
  {
    public override Type ComponentType =>
      FindComponentType(typeof(OzdsSummaryComponentBase<TModel>));

    public override ModelComponentKind ComponentKind =>
      ModelComponentKind.Summary;
  }
}
