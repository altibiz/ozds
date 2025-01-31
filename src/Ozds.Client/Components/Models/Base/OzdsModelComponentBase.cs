using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Base;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

// NITPICK: decouple from IModelComponentProvider

public abstract partial class OzdsModelComponentBase<TModel>
  : OzdsComponentBase, IModelComponentProvider
{
  private Type? baseComponentType;

  private Dictionary<string, object>? baseParameters;

  [Parameter]
  public bool Isolate { get; set; }

  [Inject]
  protected ModelComponentProvider Provider { get; set; } = default!;

  public virtual Type ModelType
  {
    get { return typeof(TModel); }
  }

  public bool CanRender(Type type)
  {
    return type.IsAssignableTo(ModelType);
  }

  public virtual Type ComponentType
  {
    get { return GetType(); }
  }

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

    return Provider.GetComponentType(baseModelType, ComponentKind);
  }

  protected virtual Type CreateBaseComponentType()
  {
    var baseModelType = ModelType.BaseType;
    if (baseModelType is null)
    {
      throw new InvalidOperationException(
        $"No base type found for {ModelType.FullName}");
    }

    return Provider.GetComponentType(baseModelType, ComponentKind);
  }

  protected Dictionary<string, object> BaseParameters()
  {
    return baseParameters ??= CreateFixedBaseParameters();
  }

  protected Dictionary<string, object> BaseParameters(
    RenderFragment childContent
  )
  {
    return CreateFixedBaseParameters(childContent);
  }

  protected virtual Dictionary<string, object> CreateBaseParameters()
  {
    return new Dictionary<string, object>();
  }

  private Dictionary<string, object> CreateFixedBaseParameters()
  {
    var parameters = CreateBaseParameters();
    parameters.Add(nameof(Isolate), true);
    return parameters;
  }

  private Dictionary<string, object> CreateFixedBaseParameters(
    RenderFragment childContent
  )
  {
    var parameters = CreateBaseParameters();
    parameters.Add(nameof(Isolate), true);
    parameters.Add(nameof(ChildContent), childContent);
    return parameters;
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (baseParameters is not null)
    {
      baseParameters = CreateFixedBaseParameters();
    }

    if (baseComponentType is not null)
    {
      baseComponentType = CreateBaseComponentType();
    }
  }
}
