namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsListModelComponentBase<TModel> :
  OzdsModelComponentBase<TModel>
{
  protected override Type CreateBaseComponentType()
  {
    var baseModelType = ModelType.BaseType;
    if (baseModelType is null)
    {
      throw new InvalidOperationException(
        $"No base type found for {ModelType.FullName}");
    }

    return Cache.GetGenericComponentType(
      ModelType,
      baseModelType,
      ComponentKind
    );
  }

  protected override Type CreateBaseComponentType(Type baseModelType)
  {
    if (!baseModelType.IsAssignableFrom(typeof(TModel)))
    {
      throw new InvalidOperationException(
        $"{baseModelType} is not assignable to {typeof(TModel)}");
    }

    return Cache.GetGenericComponentType(
      ModelType,
      baseModelType,
      ComponentKind
    );
  }
}
