namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsListModelComponentBase<TModel> :
  OzdsModelComponentBase<TModel>
{
  protected override Type CreateBaseComponentType()
  {
    var constraintType = ModelType.BaseType;
    if (constraintType is null)
    {
      constraintType = ModelType;
    }

    return Cache.GetGenericComponentType(
      ModelType,
      constraintType,
      ComponentKind
    );
  }
}
