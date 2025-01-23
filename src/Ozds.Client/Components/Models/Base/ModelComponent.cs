using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract partial class ModelComponent : ComponentBase
{
  [Parameter]
  public bool Isolate { get; set; } = false;

  protected virtual Type ModelType =>
    modelType ??= CreateModelType();

  private Type? modelType;

  protected abstract Type CreateModelType();

  protected abstract ModelComponentKind ComponentKind { get; }

  protected Dictionary<string, object> Parameters =>
    parameters ??= CreateFixedParameters();

  private Dictionary<string, object>? parameters;

  protected virtual Dictionary<string, object> CreateParameters()
  {
    return new();
  }

  private Dictionary<string, object> CreateFixedParameters()
  {
    var fixedParameters = CreateParameters();
    fixedParameters.Add(
      nameof(OzdsModelComponentBase<object>.Isolate),
      Isolate);
    return fixedParameters;
  }

  [Inject]
  private ModelComponentProviderCache Cache { get; set; } = default!;

  private Type ComponentType =>
    componentType ??= CreateComponentType();

  private Type? componentType;

  private Type CreateComponentType()
  {
    return Cache.GetComponentType(ModelType, ComponentKind);
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();

    if (componentType is { })
    {
      componentType = CreateComponentType();
    }

    if (parameters is { })
    {
      parameters = CreateFixedParameters();
    }

    if (modelType is { })
    {
      modelType = CreateModelType();
    }
  }
}
