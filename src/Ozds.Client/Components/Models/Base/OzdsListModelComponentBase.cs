namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsListModelComponentBase<TModel, TConstraintType> :
  OzdsModelComponentBase<TModel>
  where TModel : TConstraintType
{
  protected override Type CreateBaseComponentType()
  {
    return Cache.GetGenericComponentType(
      ModelType,
      typeof(TConstraintType),
      ComponentKind
    );
  }
}
