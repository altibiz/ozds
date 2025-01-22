using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class ManagedModelComponent : ModelComponent
{
  [Parameter]
  public object Model { get; set; } = default!;

  protected override Type ModelType => Model.GetType();

  protected override Dictionary<string, object> Parameters =>
    parameters ??= CreateParameters();

  private Dictionary<string, object>? parameters;

  private Dictionary<string, object> CreateParameters()
  {
    return new()
    {
      ["Model"] = Model
    };
  }

  protected override void OnParametersSet()
  {
    base.OnParametersSet();
    parameters = CreateParameters();
  }
}
