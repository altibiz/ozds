using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class ListModelComponent<TPrefix, TModel> : ModelComponent
{
  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  [Parameter]
  public Expression<Func<TPrefix, TModel?>>? Prefix { get; set; } = default!;

  protected override Type CreateModelType()
  {
    return typeof(TModel);
  }

  protected override Type CreateComponentType()
  {
    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
      ModelType,
      typeof(TModel),
      ComponentKind
    );
  }

  protected override Dictionary<string, object> CreateParameters()
  {
    return new()
    {
      [nameof(OzdsListModelComponentBase<object, object>.Models)] = Models!,
      [nameof(OzdsListModelComponentBase<object, object>.Prefix)] = Prefix!,
    };
  }
}
