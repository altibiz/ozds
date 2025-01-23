using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Models.Base;

public abstract class OzdsManagedModelComponentBase<TModel> :
  OzdsModelComponentBase<TModel>
{
  [Parameter]
  public TModel Model { get; set; } = default!;

  protected override Dictionary<string, object> CreateBaseParameters()
  {
    return new()
    {
      { "Model", Model! }
    };
  }
}
