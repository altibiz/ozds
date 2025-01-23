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
      [nameof(OzdsManagedModelComponentBase<object>.Model)] = Model
    };
  }
}
