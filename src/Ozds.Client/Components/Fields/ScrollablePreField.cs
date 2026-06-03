using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Fields;

public partial class ScrollablePreField : OzdsComponentBase
{
  private bool _expanded;

  [Parameter]
  public string? Content { get; set; }

  [Parameter]
  public int MaxHeight { get; set; } = 600;

  [Parameter]
  public bool WrapText { get; set; }

  [Parameter]
  public EventCallback<bool> ExpandedChanged { get; set; }

  private string ScrollStyle
  {
    get
    {
      return _expanded
        ? "overflow-y: auto; overscroll-behavior: none;"
        : $"max-height: {MaxHeight}px; overflow-y: auto; overscroll-behavior: none;";
    }
  }

  private static string HeaderStyle =>
    "min-height: 48px;"
    + " background-color: var(--mud-palette-background-gray);"
    + " border-bottom: 1px solid var(--mud-palette-lines-default);";

  private string ExpandIcon
  {
    get
    {
      return _expanded
        ? Icons.Material.Filled.UnfoldLess
        : Icons.Material.Filled.UnfoldMore;
    }
  }

  private string ExpandTitle
  {
    get { return _expanded ? Translate("Collapse") : Translate("Expand"); }
  }

  private async Task ToggleExpanded()
  {
    _expanded = !_expanded;
    await ExpandedChanged.InvokeAsync(_expanded);
  }
}
