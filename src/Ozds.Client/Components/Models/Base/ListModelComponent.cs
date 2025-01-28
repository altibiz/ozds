using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Models.Base;

public abstract class ListModelComponent<TPrefix, TModel> : ModelComponent
{
  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  [Parameter]
  public Expression<Func<TPrefix, TModel?>>? Prefix { get; set; } = default!;

  protected override Type CreateModelType()
  {
    var prefixFunc = Prefix?.Compile() ?? ((TPrefix x) => (TModel?)(object?)x);
    var modelType = Models
      .Select(prefixFunc)
      .Select(x => x?.GetType())
      .OfType<Type>()
      .CommonType();
    return modelType ?? typeof(TModel);
  }

  protected override Type CreateComponentType()
  {
    return Provider.GetPrefixedComponentType(
      typeof(TPrefix),
      ModelType,
      ModelType,
      ComponentKind
    );
  }

  protected override Dictionary<string, object> CreateParameters()
  {
    return new()
    {
      { nameof(OzdsListModelComponentBase<object, object>.Models), Models! },
      { nameof(OzdsListModelComponentBase<object, object>.Prefix), Prefix! },
    };
  }
}
