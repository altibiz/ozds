using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

// NITPICK: decouple from IModelComponentProvider

public abstract partial class OzdsModelComponentBase<TModel>
  : OzdsComponentBase, IModelComponentProvider
{
  [Parameter]
  public bool IsTopLevel { get; set; } = true;

  [Inject]
  protected ModelComponentProviderCache Cache { get; set; } = default!;

  public virtual Type ModelType => typeof(TModel);

  public bool CanRender(Type type)
  {
    return type.IsAssignableTo(ModelType);
  }

  public virtual Type ComponentType => GetType();

  public abstract ModelComponentKind ComponentKind { get; }

  protected Type BaseComponentType()
  {
    return baseComponentType ??= CreateBaseComponentType();
  }

  protected Type BaseComponentType<T>()
  {
    return CreateBaseComponentType(typeof(T));
  }

  protected Type BaseComponentType(Type baseModelType)
  {
    return CreateBaseComponentType(baseModelType);
  }

  protected virtual Type CreateBaseComponentType(Type baseModelType)
  {
    if (!baseModelType.IsAssignableFrom(ModelType))
    {
      throw new InvalidOperationException(
        $"{baseModelType} is not assignable from {ModelType}");
    }

    return Cache.GetComponentType(baseModelType, ComponentKind);
  }

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

  protected Dictionary<string, object> BaseParameters()
  {
    return baseParameters ??= CreateFixedBaseParameters();
  }

  private Dictionary<string, object>? baseParameters;

  protected virtual Dictionary<string, object> CreateBaseParameters()
  {
    return new();
  }

  private Dictionary<string, object> CreateFixedBaseParameters()
  {
    var parameters = CreateBaseParameters();
    parameters.Add(nameof(IsTopLevel), false);
    return parameters;
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (baseParameters is { })
    {
      baseParameters = CreateFixedBaseParameters();
    }

    if (baseComponentType is { })
    {
      baseComponentType = CreateBaseComponentType();
    }
  }
}
