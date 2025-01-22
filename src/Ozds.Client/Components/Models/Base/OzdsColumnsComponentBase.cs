using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsColumnsComponentBase<TModel> :
  OzdsModelComponentBase<TModel>
{
  public class ModelComponentProvider : ModelComponentProviderBase
  {
    public override Type ComponentType =>
      FindComponentType(typeof(OzdsColumnsComponentBase<TModel>));

    public override ModelComponentKind ComponentKind =>
      ModelComponentKind.Columns;
  }
}
