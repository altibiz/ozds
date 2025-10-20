using Microsoft.AspNetCore.Components;
using Ozds.Business.Reflection;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

/*
  NOTE: we have to keep track of the type here because
  edit components mutate models and don't actually set them so
  it doesn't trigger subsequent SetParameters on child components
*/

public partial class PolymorphicIdField : OzdsComponentBase
{
  private string? type;

  [Parameter]
  public string TypeLabel { get; set; } = default!;

  [Parameter]
  public string Type { get; set; } = default!;

  [Parameter]
  public EventCallback<string> TypeChanged { get; set; } = default!;

  [Parameter]
  public IEnumerable<Type> TypeChoices { get; set; } = default!;

  [Parameter]
  public string IdLabel { get; set; } = default!;

  [Parameter]
  public string Id { get; set; } = default!;

  [Parameter]
  public EventCallback<string> IdChanged { get; set; } = default!;

  [Inject]
  public ModelReflector ModelReflector { get; set; } = default!;

  protected override void OnParametersSet()
  {
    type = Type;
  }

  private Task OnTypeChanged(string? value)
  {
    type = value;
    return TypeChanged.InvokeAsync(value);
  }
}
