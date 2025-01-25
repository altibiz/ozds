using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsPrefixedModelComponentBase<TPrefix, TModel> :
  OzdsModelComponentBase<TModel>
{
  [Parameter]
  public Expression<Func<TPrefix, TModel?>>? Prefix { get; set; } = default!;

  protected override Type CreateBaseComponentType()
  {
    var baseModelType = ModelType.BaseType;
    if (baseModelType is null)
    {
      throw new InvalidOperationException(
        $"No base type found for {ModelType.FullName}");
    }

    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
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

    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
      ModelType,
      baseModelType,
      ComponentKind
    );
  }
}
