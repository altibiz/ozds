using Microsoft.AspNetCore.Components;

namespace Ozds.Client.Components.Fields;

public partial class MonthPickerField
{
  [Parameter]
  public DateTime Value { get; set; }

  [Parameter]
  public EventCallback<DateTime> ValueChanged { get; set; }

  [Parameter]
  public string Label { get; set; } = "Pick month";

  private DateTime? NullableValue
  {
    get { return Value; }
    set
    {
      if (value.HasValue && value.Value != Value)
      {
        ValueChanged.InvokeAsync(value.Value);
      }
    }
  }
}
