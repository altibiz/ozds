using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Ozds.Client.Components.Fields;

public partial class PasswordField
{
  private string inputIcon = Icons.Material.Filled.VisibilityOff;

  private InputType inputType = InputType.Password;

  private bool show;

  private string? text;

  [Parameter]
  public string Label { get; set; } = default!;

  [Parameter]
  public string? Value { get; set; }

  [Parameter]
  public EventCallback<string?> ValueChanged { get; set; }

  [Parameter]
  public Expression<Func<string?>>? For { get; set; }

  private void OnVisibilityClick()
  {
    if (show)
    {
      show = false;
      inputIcon = Icons.Material.Filled.VisibilityOff;
      inputType = InputType.Password;
    }
    else
    {
      show = true;
      inputIcon = Icons.Material.Filled.Visibility;
      inputType = InputType.Text;
    }
  }
}
