using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class DateTimeRangePickerField : OzdsComponentBase
{
  [Parameter]
  public DateTimeOffset? Start { get; set; }

  [Parameter]
  public EventCallback<DateTimeOffset?> StartChanged { get; set; }

  [Parameter]
  public DateTimeOffset? End { get; set; }

  [Parameter]
  public EventCallback<DateTimeOffset?> EndChanged { get; set; }

  [Parameter]
  public string? Label { get; set; }

  [Parameter]
  public string? StartLabel { get; set; }

  [Parameter]
  public string? EndLabel { get; set; }

  [Parameter]
  public string? TimeLabel { get; set; }

  [Parameter]
  public DateTimeOffset? MinDate { get; set; }

  [Parameter]
  public DateTimeOffset? MaxDate { get; set; }

  [Parameter]
  public bool Clearable { get; set; } = true;

  [Parameter]
  public bool Disabled { get; set; }

  [Parameter]
  public Variant Variant { get; set; } = Variant.Text;

  [Parameter]
  public Margin Margin { get; set; } = Margin.None;

  [Parameter]
  public string? Class { get; set; }

  [Parameter]
  public string? Style { get; set; }

  private async Task OnStartChanged(DateTimeOffset? value)
  {
    await StartChanged.InvokeAsync(value);

    // Keep the range valid: the start must never come after the end.
    if (value is { } start && End is { } end && start > end)
    {
      await EndChanged.InvokeAsync(value);
    }
  }

  private async Task OnEndChanged(DateTimeOffset? value)
  {
    await EndChanged.InvokeAsync(value);

    // Keep the range valid: the end must never come before the start.
    if (value is { } end && Start is { } start && end < start)
    {
      await StartChanged.InvokeAsync(value);
    }
  }

  private async Task Clear()
  {
    await StartChanged.InvokeAsync(null);
    await EndChanged.InvokeAsync(null);
  }
}
