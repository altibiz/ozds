using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class ManagedModelComponent : ModelComponent
{
  [Parameter]
  public object Model { get; set; } = default!;

  protected override Type CreateModelType()
  {
    return Model.GetType();
  }

  protected override Dictionary<string, object> CreateParameters()
  {
    return new()
    {
      { nameof(OzdsManagedModelComponentBase<object>.Model), Model }
    };
  }
}

public abstract class ManagedModelComponent<TPrefix, TModel> : ModelComponent
{
  [Parameter]
  public TPrefix Model { get; set; } = default!;

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
      { nameof(OzdsManagedModelComponentBase<object, object>.Model), Model! },
      { nameof(OzdsManagedModelComponentBase<object, object>.Prefix), Prefix! },
    };
  }
}
