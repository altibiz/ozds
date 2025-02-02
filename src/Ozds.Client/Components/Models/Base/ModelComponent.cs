using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract partial class ModelComponent : ComponentBase
{
  private Type? componentType;

  private Type? modelType;

  private Dictionary<string, object>? parameters;

  [Parameter]
  public bool Isolate { get; set; }

  protected virtual Type ModelType
  {
    get { return modelType ??= CreateModelType(); }
  }

  protected abstract ModelComponentKind ComponentKind { get; }

  protected Dictionary<string, object> Parameters
  {
    get { return parameters ??= CreateFixedParameters(); }
  }

  [Inject]
  protected ModelComponentProvider Provider { get; set; } = default!;

  private Type ComponentType
  {
    get { return componentType ??= CreateComponentType(); }
  }

  protected abstract Type CreateModelType();

  protected virtual Dictionary<string, object> CreateParameters()
  {
    return new Dictionary<string, object>();
  }

  private Dictionary<string, object> CreateFixedParameters()
  {
    var fixedParameters = CreateParameters();
    fixedParameters.Add(
      nameof(OzdsModelComponentBase<object>.Isolate),
      Isolate);
    return fixedParameters;
  }

  protected virtual Type CreateComponentType()
  {
    return Provider.GetComponentType(ModelType, ComponentKind);
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (modelType is not null)
    {
      modelType = CreateModelType();
    }

    if (parameters is not null)
    {
      parameters = CreateFixedParameters();
    }

    if (componentType is not null)
    {
      componentType = CreateComponentType();
    }
  }
}
