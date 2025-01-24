using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsListModelComponentBase<TPrefix, TModel> :
  OzdsModelComponentBase<TModel>
{
  [Parameter]
  public IEnumerable<TPrefix> Models { get; set; } = default!;

  [Parameter]
  public Func<TPrefix, TModel>? Prefix { get; set; } = default!;

  protected Func<TPrefix, TModel> Fix =>
    fix ??= CreateFix();

  private Func<TPrefix, TModel>? fix;

  protected Func<TPrefix, TModel> CreateFix()
  {
    return Prefix ?? ((TPrefix x) => (TModel)(object)x!);
  }

  protected override Dictionary<string, object> CreateBaseParameters()
  {
    return new()
    {
      { nameof(Models), Models! },
      { nameof(Prefix), Prefix! }
    };
  }

  protected override Type CreateBaseComponentType()
  {
    var baseModelType = ModelType.BaseType;
    if (baseModelType is null)
    {
      throw new InvalidOperationException(
        $"No base type found for {ModelType.FullName}");
    }

    return Cache.GetPrefixedComponentType(
      ModelType,
      baseModelType,
      typeof(TPrefix),
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

    return Cache.GetPrefixedComponentType(
      ModelType,
      baseModelType,
      typeof(TPrefix),
      ComponentKind
    );
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (fix is { })
    {
      fix = CreateFix();
    }
  }
}
