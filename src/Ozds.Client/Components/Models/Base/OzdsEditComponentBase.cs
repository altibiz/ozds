using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsEditComponentBase<TModel> :
  OzdsManagedModelComponentBase<TModel>
{
  public class ModelComponentProvider : ModelComponentProviderBase
  {
    public override Type ComponentType =>
      FindComponentType(typeof(OzdsEditComponentBase<TModel>));

    public override ModelComponentKind ComponentKind =>
      ModelComponentKind.Edit;
  }
}
