using Microsoft.AspNetCore.Components;
using Ozds.Client.Components.Models.Abstractions;

namespace Ozds.Client.Components.Models.Base;

public abstract partial class ModelComponent : ComponentBase
{
  protected abstract Type ModelType { get; }

  protected abstract ModelComponentKind ComponentKind { get; }

  protected abstract Dictionary<string, object> Parameters { get; }

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
    componentType = CreateComponentType();
  }
}
