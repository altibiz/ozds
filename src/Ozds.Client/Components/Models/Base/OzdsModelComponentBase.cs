using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

// NITPICK: decouple from IModelComponentProvider

public abstract partial class OzdsModelComponentBase<TModel>
  : OzdsComponentBase, IModelComponentProvider
{
  [Inject]
  protected ModelComponentProviderCache Cache { get; set; } = default!;

  public virtual Type ModelType => typeof(TModel);

  public bool CanRender(Type type)
  {
    return type.IsAssignableTo(ModelType);
  }

  public virtual Type ComponentType => GetType();

  public abstract ModelComponentKind ComponentKind { get; }

  public Type BaseComponentType =>
    baseComponentType ??= CreateBaseComponentType();

  private Type? baseComponentType;

  protected virtual Type CreateBaseComponentType()
  {
    var baseModelType = ModelType.BaseType;
    if (baseModelType is null)
    {
      throw new InvalidOperationException(
        $"No base type found for {ModelType.FullName}");
    }

    return Cache.GetComponentType(baseModelType, ComponentKind);
  }

  public Dictionary<string, object> BaseParameters =>
    baseParameters ??= CreateBaseParameters();

  private Dictionary<string, object>? baseParameters;

  protected virtual Dictionary<string, object> CreateBaseParameters()
  {
    return new();
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (baseParameters is { })
    {
      baseParameters = CreateBaseParameters();
    }

    if (baseComponentType is { })
    {
      baseComponentType = CreateBaseComponentType();
    }
  }
}
