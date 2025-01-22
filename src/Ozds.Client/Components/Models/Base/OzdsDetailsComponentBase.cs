using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsDetailsComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public class ModelComponentProvider : ModelComponentProviderBase
  {
    public override Type ComponentType =>
      FindComponentType(typeof(OzdsDetailsComponentBase<TModel>));

    public override ModelComponentKind ComponentKind =>
      ModelComponentKind.Details;
  }
}
